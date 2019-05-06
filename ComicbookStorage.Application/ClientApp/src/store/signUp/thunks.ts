import { Dispatch } from 'redux';
import { FormErrors } from "redux-form";
import { CreateUserActionTypes, User } from './types';
import { processUserCreated } from './actions';
import comicbookServer from '../../utils/comicbookServer';

export const createUserAsync = () => (dispatch: Dispatch): Promise<CreateUserActionTypes> => {
    return new Promise(resolve => setTimeout(resolve, 1000)).then(res => dispatch(processUserCreated()));
}

export const isUniqueFieldTakenAsync = (fieldName: string, fieldValue: string, existingErrors: FormErrors<User>): Promise<boolean> => {
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