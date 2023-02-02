import { User, UserProfile } from 'oidc-client-ts';

export type UserProfileExtended = UserProfile & { role: string[]; inn: string };

export type UserExtended = Omit<User, 'profile'> & { profile: UserProfileExtended };

export type UserRole = 'manager_org_seller' | 'manager_org_buyer' | 'content_manager';
