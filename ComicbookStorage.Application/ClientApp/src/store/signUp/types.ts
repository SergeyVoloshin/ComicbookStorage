import { ValidationErrors } from '../common/types';

export interface CreatedUser {
    email: string,
    name: string,
    password: string,
    confirmPassword: string,
}

export interface CreatedUserDto {
    email: string,
    name: string,
    password: string,
}

export interface CreateUserState {
    errors?: ValidationErrors,
}

export const CREATE_USER_RESPONSE = 'CREATE_USER_RESPONSE';

export const CREATE_USER_ERROR = 'CREATE_USER_ERROR';

export interface UserCreatedAction {
    type: typeof CREATE_USER_RESPONSE,
}

export interface CreateUserFailedAction {
    type: typeof CREATE_USER_ERROR,
    errors: ValidationErrors,
}

export type CreateUserActionTypes = UserCreatedAction | CreateUserFailedAction