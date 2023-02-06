/* eslint-disable @shopify/typescript/prefer-pascal-case-enums */
enum Role {
  user = 0,
  admin = 1,
}

export interface LoginResult {
  id: string;
  token: string;
  refreshToken: string;
  roles: Role;
  expirationRefreshTokenMs: number;
  isPersist: boolean;
}

export interface LoginRequest {
  email: string;
  password: string;
  isPersist: boolean;
}

export interface User {
  token: string;
}
