import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { token } from '@/app/core/services/auth/local-storage.service';
import { IOrganizationFullModel } from '@/app/organization-full/@types/IOrganizationFullModel';
import { organizationHttpService } from '@/app/organization-full/infrastructure/organozation-full.http-service';
import { computed } from 'vue';
import type { UserExtended } from '@/types/user';

export interface IUserStore {
  user: UserExtended | null;
  org: IOrganizationFullModel | null;
  status: DataStatus;
}

export interface IUserRole {
  name: string;
  value: string;
}

export const UserRoles: IUserRole[] = [
  {
    name: 'Продавец',
    value: 'manager_org_seller',
  },
  {
    name: 'Покупатель',
    value: 'manager_org_buyer',
  },
  {
    name: 'Администратор',
    value: 'content_manager',
  },
];

export type UserRole = 'manager_org_seller' | 'manager_org_buyer' | 'content_manager';

const setUserKey = 'set-user';

const defaultState: IUserStore = {
  user: null,
  org: null,
  status: new DataStatus(),
};

const {
  state: userState,
  getter,
  mutation,
  onMutationSuccess,
  action,
  computeState,
} = defineStore('user', defaultState);

export const isAuthenticated = getter('is-auth', (state) => state.user != null);

export const accessToken = getter('get-access-token', (state) =>
  state.user == null ? null : state.user.access_token,
);

export const userInfo = getter('get-user-info', (state) =>
  state.user == null
    ? null
    : {
        name: state.user.profile.name,
        givenName: state.user.profile.given_name,
        familyName: state.user.profile.family_name,
        email: state.user.profile.email,
        accountName: state.user.profile.preferred_username,
        inn: state.user.profile.inn,
        role: state.user.profile.role,
        org: state.org as IOrganizationFullModel | null,
      },
);

export const userRoles = computed(() => userInfo.value?.role ?? []);

export const userRoleContains = (...roles: UserRole[]) =>
  getter<boolean>(['check-roles-', ...roles].join('-'), (state) => {
    if (state.user == null || state.user.profile == null) {
      return false;
    }
    return state.user.profile.role.some((r) => roles.some((role) => role === r));
  });

export const setUser = mutation(setUserKey, (state, user: UserExtended | null) => {
  state.user = user;
});

export const organization = computeState((state) => state.org);
export const status = computeState((state) => state.status);

const getOrganization = action<string>('get-user-organization', async (inn) => {
  status.value = new DataStatus('loading');
  const org = await organizationHttpService.getByInn(inn);
  if (org.status === HttpStatus.Success && org.data != null) {
    organization.value = org.data;
  }
  status.value = new DataStatus('loaded');
});

onMutationSuccess(setUserKey, (p) => {
  const user: UserExtended | null = p.payload;
  token.value = user == null ? null : user.access_token;
  if (user?.profile.inn != null && status.value.status !== 'loading') {
    getOrganization(p.payload.profile.inn);
  }
});

export { userState };
