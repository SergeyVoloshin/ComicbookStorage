import { Dispatch } from 'redux';
import { FormErrors } from "redux-form";
import { CreateUserActionTypes, CreatedUser, CreatedUserDto } from './types';
import { processUserCreated, processCreateUserFailed } from './actions';
import comicbookServer from '../../utils/comicbookServer';

export const createUserAsync = (newUser: CreatedUser) => async (dispatch: Dispatch): Promise<CreateUserActionTypes> => {
    let newUserDto: CreatedUserDto = {
        email: newUser.email,
        name: newUser.name,
        password: newUser.password
    }
    let response: Response = await comicbookServer.post('/account/create-user', newUserDto, true);

    if (response.ok) {
        return dispatch(processUserCreated());
    } else {
        return dispatch(processCreateUserFailed(await response.json()));
    }
}

export const isUniqueFieldTakenAsync = (fieldName: string, fieldValue: string, existingErrors: FormErrors<CreatedUser>): Promise<boolean> => {
    return comicbookServer.get<boolean>(`/account/is-${fieldName}-taken/${fieldValue}`, false)
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