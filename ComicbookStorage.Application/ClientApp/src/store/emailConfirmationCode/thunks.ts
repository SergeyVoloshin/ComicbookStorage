import { Dispatch } from 'redux';
import { History } from 'history';
import { ConfirmEmailActionTypes } from './types';
import { processEmailConfirmed, processConfirmEmailFailed } from './actions';
import comicbookServer from '../../utils/comicbookServer';
import AppPathConfig from "../../utils/appPathConfig";

export const confirmEmailAsync = (confirmationCode: string, history: History) => async (dispatch: Dispatch): Promise<ConfirmEmailActionTypes> => {
    let response: Response = await comicbookServer.put(`/account/confirm-email/${confirmationCode}`, false);

    if (response.ok) {
        history.push(AppPathConfig.logIn);
        return dispatch(processEmailConfirmed());
    } else {
        return dispatch(processConfirmEmailFailed(await response.json()));
    }
}