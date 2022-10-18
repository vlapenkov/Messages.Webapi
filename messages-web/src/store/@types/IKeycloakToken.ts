/* eslint-disable camelcase */
import { KeycloakTokenParsed } from 'keycloak-js';

export interface IKeycloakToken extends KeycloakTokenParsed {
  scope: string /* openid profile email */;
  sid: string;
  email_verified: false;
  name: string /* 'Данила Михайлов' */;
  preferred_username: string /* 'mikhaylov.ds' */;
  given_name: string /* 'Данила' */;
  family_name: string /* 'Михайлов' */;
  email: string /* 'mikhaylov.ds@roscosmos.digital' */;
}
