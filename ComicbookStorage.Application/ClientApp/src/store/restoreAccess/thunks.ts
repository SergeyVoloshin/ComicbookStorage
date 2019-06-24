import { Dispatch } from 'redux';
import { History } from 'history';
import { RestoreRequest, RestoreAccessActionTypes, RestoreDto } from './types';
import { processEmailSent, processRestoreFailed } from './actions';
import comicbookServer from '../../utils/comicbookServer';
import AppPathConfig from "../../utils/appPathConfig";
import messageBox from "../../utils/messageBox";

export const restoreAccessAsync = (restoreRequest: RestoreRequest, history: History) => async (dispatch: Dispatch): Promise<RestoreAccessActionTypes> => {
    let restoreDto: RestoreDto = {
        email: restoreRequest.email,
    }
    let response: Response = await comicbookServer.post('/account/restore', restoreDto, false, true);

    if (response.ok) {
        messageBox.showInfo("Please check your email!");
        history.push(AppPathConfig.logIn);
        return dispatch(processEmailSent());
    } else {
        return dispatch(processRestoreFailed(await response.json()));
    }
}