export interface CreateUserReq {
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  joinedDate: string;
  gender: number;
  role: number;
  location: number;
}

export interface UpdateUserReq {
  dateOfBirth: string;
  joinedDate: string;
  gender: number;
  role: number;
  location: number;
  userId: string;
}

export interface LoginReq {
  username: string;
  password: string;
}

export interface FirstTimeLoginReq {
  username: string;
  currentPassword: string;
  newPassword: string;
}

export interface UserRes {
  id: string;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  joinedDate: string;
  gender: number;
  role: number;
  staffCode: string;
  username: string;
  location?: number;
  createdOn: string;
}

export interface GetUserReq {
  token: string;
  pageSize: number;
  pageNumber: number;
  search?: string;
  roleType?: string;
}

export type PaginationState = {
  pageIndex: number;
  pageSize: number;
};
