import { Dispatch } from 'redux';
import { History } from 'history';
import { LogInActionTypes, LogInRequest, LogInDto } from './types';
import { processLoggedIn, processLogInFailed } from './actions';
import comicbookServer from '../../utils/comicbookServer';
import AppPathConfig from "../../utils/appPathConfig";

export const logInAsync = (logInRequest: LogInRequest, history: History) => async (dispatch: Dispatch): Promise<LogInActionTypes> => {
    let logInDto: LogInDto = {
        email: logInRequest.email,
        password: logInRequest.password
    }
    let response: Response = await comicbookServer.post('/account/log-in', logInDto, false, true);

    if (response.ok) {
        history.push(AppPathConfig.home);
        comicbookServer.setAuthenticationTokens(await response.json());
        return dispatch(processLoggedIn());
    } else {
        return dispatch(processLogInFailed(await response.json()));
    }
}