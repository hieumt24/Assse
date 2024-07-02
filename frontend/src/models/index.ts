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
  staffCode: string;
  username: string;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  joinedDate: Date;
  gender: number;
  role: number;
  location?: number;
  createdOn: string;
}

export interface AssetRes {
  id?: string;
  assetCode?: string;
  assetName?: string;
  specification?: string;
  installedDate?: Date;
  state?: number;
  assetLocation?: number;
  categoryName?: string;
}

export interface CategoryRes {
  categoryName: string;
  prefix: string;
  id: string;
  createdOn: string;
}

export interface AssignmentRes {
  id?: string;
  assetCode?: string;
  assetName?: string;
  assignedTo?: string;
  assignedBy?: string;
  assignedDate?: Date;
  location?: number;
  state?: number;
  isRequested?: boolean;
}
export interface GetAssignemntReq {
  pagination: {
    pageSize: number;
    pageIndex: number;
  };
  assignmentState?: number;
  assignedDate?: string;
  search?: string;
  orderBy?: string;
  adminLocation?: number;
  isDescending?: boolean;
}

export interface GetAssignemntByUserAssignedReq {
  pagination: {
    pageSize: number;
    pageIndex: number;
  };
  assignmentState?: number;
  assignedDate?: string;
  search?: string;
  orderBy?: string;
  isDescending?: boolean;
  userId: string;
}

export interface GetUserReq {
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

export interface GetAssetReq {
  pagination: {
    pageSize: number;
    pageIndex: number;
  };
  search?: string;
  categoryId?: string;
  assetStateType?: Array<number>;
  adminLocation?: number;
  orderBy?: string;
  isDescending?: boolean;
}

export type PaginationState = {
  pageIndex: number;
  pageSize: number;
};

export interface CreateCategoryReq {
  categoryName: string;
  prefix: string;
}

export interface CreateAssetReq {
  adminId: string;
  assetName: string;
  specification: string;
  installedDate: string;
  state: number;
  assetLocation: number;
  categoryId: string;
}

export interface UpdateAssetReq {
  assetName: string;
  specification: string;
  installedDate: string;
  state: number;
}

export interface AssetRes {
  id?: string;
  assetCode?: string;
  assestName?: string;
  specification?: string;
  installedDate?: Date;
  state?: number;
  assetLocation?: number;
  categoryName?: string;
}

export interface CreateAssignmentReq {
  assignedIdTo: string;
  assignedIdBy: string;
  assetId: string;
  assignedDate: string;
  note: string;
  location: number;
  state: number;
}

export interface UpdateAssignmentStateReq {
  assignmentId: string;
  newState: number;
}

export interface ReturningRequestRes {
  id: string;
  assetCode: string;
  assetName: string;
  requestedByUsername: string;
  assignedDate: Date;
  acceptedByUsername?: string;
  returnedDate?: Date;
  state: number;
  location: number;
}

export interface GetReturningRequestReq {
  pagination: {
    pageSize: number;
    pageIndex: number;
  };
  search?: string;
  orderBy?: string;
  isDescending?: boolean;
  returnState?: number;
  returnedDate?: string;
  location?: number;
}

export interface CreateReturningRequestReq {
  assignmentId?: string;
  requestedBy?: string;
  returnedDate?: string;
  location?: number;
}

export interface UpdateReturningRequestReq {
  returnRequestId?: string;
  newState?: number;
}
