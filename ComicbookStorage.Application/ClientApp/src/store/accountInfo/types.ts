import { ValidationErrors } from '../common/types';

export interface UpdatedUser {
    email: string,
    name: string,
    oldPassword: string,
    password: string,
    confirmPassword: string,
}

export interface UpdatedUserDto {
    email: string,
    name: string,
    oldPassword: string,
    newPassword: string,
}

export interface UpdatedUserResultDto {
    isConfirmationRequired: boolean,
}

export interface InitialUserDataDto {
    email: string,
    name: string,
}

export interface UpdateUserState {
    userData?: UpdatedUser,
    errors?: ValidationErrors,
}

export const UPDATE_USER_SUCCESS = 'UPDATE_USER_SUCCESS';

export const UPDATE_USER_ERROR = 'UPDATE_USER_ERROR';

export const UPDATE_USER_RETRIEVE_CURRENT = 'UPDATE_USER_RETRIEVE_CURRENT';

export interface UserUpdatedAction {
    type: typeof UPDATE_USER_SUCCESS,
}

export interface UpdateUserFailedAction {
    type: typeof UPDATE_USER_ERROR,
    errors: ValidationErrors,
}

export interface UserInitialDataReceivedAction {
    type: typeof UPDATE_USER_RETRIEVE_CURRENT,
    userData: UpdatedUser,
}

export type UpdateUserActionTypes = UserUpdatedAction | UpdateUserFailedAction | UserInitialDataReceivedAction