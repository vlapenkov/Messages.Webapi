import { User, UserProfile } from 'oidc-client-ts';

export type UserProfileExtended = UserProfile & { role: string[]; inn: string };

export type UserExtended = Omit<User, 'profile'> & { profile: UserProfileExtended };
