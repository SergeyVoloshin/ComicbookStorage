import { Dispatch } from 'redux';
import { History } from 'history';
import { ConfirmEmailActionTypes } from './types';
import { processEmailConfirmed, processConfirmEmailFailed } from './actions';
import comicbookServer from '../../utils/comicbookServer';
import AppPathConfig from "../../utils/appPathConfig";
import messageBox from "../../utils/messageBox";

export const confirmEmailAsync = (confirmationCode: string, history: History) => async (dispatch: Dispatch): Promise<ConfirmEmailActionTypes> => {
    let response: Response = await comicbookServer.put(`/account/confirm-email/${confirmationCode}`, null, false, false);

    if (response.ok) {
        messageBox.showInfo('The email address has been confirmed. Please log in');
        history.push(AppPathConfig.logIn);
        return dispatch(processEmailConfirmed());
    } else {
        return dispatch(processConfirmEmailFailed(await response.json()));
    }
}