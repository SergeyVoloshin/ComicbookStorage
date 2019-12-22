import { LoggedInAction, LOG_IN_SUCCESS, LogInFailedAction, LOG_IN_ERROR, LogOutAction, LOG_OUT, LogOutReason } from './types';
import { ErrorResponse } from '../common/types';
import comicbookServer from '../../utils/comicbookServer';
import messageBox from "../../utils/messageBox";

export function processLoggedIn(): LoggedInAction {
    return {
        type: LOG_IN_SUCCESS,
    }
}

export function logOut(reason: LogOutReason = LogOutReason.Timeout): LogOutAction {
    if (reason === LogOutReason.Timeout) {
        messageBox.showInfo("You have been logged out due to timeout");
    } else if (reason === LogOutReason.UserRequest) {
        messageBox.showInfo("You have been successfully logged out");
    }
    comicbookServer.clearAuthenticationTokens();
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