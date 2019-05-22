import { UserCreatedAction, CREATE_USER_SUCCESS, CreateUserFailedAction, CREATE_USER_ERROR } from './types';
import { ErrorResponse } from '../common/types';

export function processUserCreated(): UserCreatedAction {
    return {
        type: CREATE_USER_SUCCESS,
    }
}

export function processCreateUserFailed(errorResponse: ErrorResponse): CreateUserFailedAction {
    return {
        type: CREATE_USER_ERROR,
        errors: errorResponse.errors,
    }
}