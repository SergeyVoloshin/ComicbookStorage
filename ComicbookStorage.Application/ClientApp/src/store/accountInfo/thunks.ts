import { Dispatch } from 'redux';
import { History } from 'history';
import { UserInitialDataReceivedAction, UpdatedUser, UpdateUserActionTypes, UpdatedUserDto } from './types';
import { receiveInitialUserData, processUserUpdated, processUpdateUserFailed } from './actions';
import comicbookServer from '../../utils/comicbookServer';

export const requestInitialUserDataAsync = () => (dispatch: Dispatch): Promise<UserInitialDataReceivedAction> => {
    return comicbookServer.get("/account/get-user-data", true, true)
        .then(response => response.json())
        .then(json => dispatch(receiveInitialUserData(json)));
}

export const updateUserAsync = (user: UpdatedUser, history: History) => async (dispatch: Dispatch): Promise<UpdateUserActionTypes> => {
    let updatedUserDto: UpdatedUserDto = {
        email: user.email,
        name: user.name,
        oldPassword: user.oldPassword,
        newPassword: user.password
    }
    let response: Response = await comicbookServer.post('/account/update-user', updatedUserDto, true, true);

    if (response.ok) {
        return dispatch(processUserUpdated(await response.json(), history));
    } else {
        return dispatch(processUpdateUserFailed(await response.json()));
    }
}