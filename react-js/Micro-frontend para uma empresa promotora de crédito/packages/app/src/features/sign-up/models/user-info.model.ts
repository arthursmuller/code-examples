export interface UserInfo {
  name: string;
  surname: string;
  cpf: string;
  phone: string;
}

export interface UserSecurityInfo {
  email: string;
  password: string;
  repeatPassword: string;
  currentPassword?: string;
}

export interface UserData extends UserInfo, UserSecurityInfo {}
