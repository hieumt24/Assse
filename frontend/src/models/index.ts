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
  joinedDate: Date;
  gender: number;
  role: number;
  staffCode: string;
  username: string;
  location?: number;
  createdOn: string;
}

export interface CategoryRes {
  categoryName: string;
  prefix: string;
  id: string;
  createdOn: string;
}

export interface GetUserReq {
  token: string;
  pagination: {
    pageSize: number;
    pageIndex: number;
  };
  search?: string;
  roleType?: number;
  orderBy?: string;
  adminLocation?: number;
  isDescending?: boolean;
}

export type PaginationState = {
  pageIndex: number;
  pageSize: number;
};
