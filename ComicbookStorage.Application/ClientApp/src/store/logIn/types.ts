import { ValidationErrors } from '../common/types';

export interface LogInRequest {
    email: string,
    password: string,
}

export interface LogInDto {
    email: string,
    password: string,
}

export interface LogInState {
    authenticated: boolean,
    errors?: ValidationErrors,
}

export const LOG_IN_SUCCESS = 'LOG_IN_SUCCESS';

export const LOG_IN_ERROR = 'LOG_IN_ERROR';

export const LOG_OUT = 'LOG_OUT';

export interface LoggedInAction {
    type: typeof LOG_IN_SUCCESS,
}

export interface LogOutAction {
    type: typeof LOG_OUT,
}

export interface LogInFailedAction {
    type: typeof LOG_IN_ERROR,
    errors: ValidationErrors,
}

export type LogInActionTypes = LoggedInAction | LogInFailedAction | LogOutAction