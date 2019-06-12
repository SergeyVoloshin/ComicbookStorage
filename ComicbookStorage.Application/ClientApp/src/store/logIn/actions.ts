import { LoggedInAction, LOG_IN_SUCCESS, LogInFailedAction, LOG_IN_ERROR, LogOutAction, LOG_OUT } from './types';
import { ErrorResponse } from '../common/types';
import comicbookServer from '../../utils/comicbookServer';

export function processLoggedIn(): LoggedInAction {
    return {
        type: LOG_IN_SUCCESS,
    }
}

export function logOut(): LogOutAction {
    comicbookServer.clearAuthenticationToken();
    return {
        type: LOG_OUT,
    }
}

export function processLogInFailed(errorResponse: ErrorResponse): LogInFailedAction {
    return {
        type: LOG_IN_ERROR,
        errors: errorResponse.errors,
    }
}