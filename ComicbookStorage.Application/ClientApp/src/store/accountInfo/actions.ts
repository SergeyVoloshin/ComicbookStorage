import { Dispatch } from 'redux';
import { History } from 'history';
import { UserUpdatedAction, UPDATE_USER_SUCCESS, UpdateUserFailedAction, UPDATE_USER_ERROR, InitialUserDataDto, UPDATE_USER_RETRIEVE_CURRENT, UserInitialDataReceivedAction, UpdatedUser, UpdatedUserResultDto } from './types';
import { ErrorResponse } from '../common/types';
import messageBox from "../../utils/messageBox";
import AppPathConfig from "../../utils/appPathConfig";
import { logOut } from '../logIn/actions';
import { LogOutReason } from '../logIn/types';


export function processUserUpdated(result: UpdatedUserResultDto, history: History, dispatch: Dispatch): UserUpdatedAction {
    if (result.isConfirmationRequired) {
        dispatch(logOut(LogOutReason.EmailChanged));
        history.push(AppPathConfig.confirmEmail);
    } else {
        messageBox.showInfo("Your account settings have been successfully updated");
    }
    return {
        type: UPDATE_USER_SUCCESS,
    }
}

export function processUpdateUserFailed(errorResponse: ErrorResponse): UpdateUserFailedAction {
    return {
        type: UPDATE_USER_ERROR,
        errors: errorResponse.errors,
    }
}

export function receiveInitialUserData(json: InitialUserDataDto): UserInitialDataReceivedAction {
    let userData: UpdatedUser = {
        email: json.email,
        name: json.name,
        oldPassword: "",
        password: "",
        confirmPassword: "",
    }
    return {
        type: UPDATE_USER_RETRIEVE_CURRENT,
        userData: userData
    }
}
