import { Dispatch } from 'redux';
import { FormErrors } from "redux-form";
import { History } from 'history';
import { CreateUserActionTypes, CreatedUser, CreatedUserDto } from './types';
import { processUserCreated, processCreateUserFailed } from './actions';
import comicbookServer from '../../utils/comicbookServer';
import AppPathConfig from "../../utils/appPathConfig";

export const createUserAsync = (newUser: CreatedUser, history: History) => async (dispatch: Dispatch): Promise<CreateUserActionTypes> => {
    let newUserDto: CreatedUserDto = {
        email: newUser.email,
        name: newUser.name,
        password: newUser.password
    }
    let response: Response = await comicbookServer.post('/account/create-user', newUserDto, false, true);

    if (response.ok) {
        history.push(AppPathConfig.confirmEmail);
        return dispatch(processUserCreated());
    } else {
        return dispatch(processCreateUserFailed(await response.json()));
    }
}

export const isUniqueFieldTakenAsync = (fieldName: string, fieldValue: string, existingErrors: FormErrors<CreatedUser>): Promise<boolean> => {
    return comicbookServer.get<boolean>(`/account/is-${fieldName}-taken/${fieldValue}`, false, false)
        .then(result => {
            if (result) {
                throw {
                    ...existingErrors,
                    [fieldName]: `That ${fieldName} is taken`
                }
            }
            if (existingErrors) {
                throw existingErrors;
            }
            return true;
        });
}