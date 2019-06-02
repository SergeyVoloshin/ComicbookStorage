import { applyMiddleware, combineReducers, compose, createStore, Store } from 'redux';
import thunk from 'redux-thunk';
import { routerReducer, routerMiddleware, RouterState } from 'react-router-redux';
import { reducer as formReducer } from 'redux-form';
import { composeWithDevTools } from 'redux-devtools-extension';
import { History } from 'history';
import { comicbookListReducer } from './comicbookList/reducer';
import { progressBarReducer } from './commonUi/reducer';
import { createUserReducer } from './signUp/reducer';
import { confirmEmailReducer } from './emailConfirmationCode/reducer';
import { ComicbookListState } from './comicbookList/types';
import { CommonUiState } from './commonUi/types';
import { CreateUserState } from './signUp/types';
import { ConfirmEmailState } from './emailConfirmationCode/types';

export default function configureStore(history: History, initialState: ApplicationState): Store<ApplicationState> {
    const reducers = {
        form: formReducer,
        comicbookList: comicbookListReducer,
        signUp: createUserReducer,
        commonUi: progressBarReducer,
        confirmEmail: confirmEmailReducer,
    };

    let middleware = applyMiddleware(thunk, routerMiddleware(history));

    const isDevelopment = process.env.NODE_ENV === 'development';
    if (isDevelopment) {
        middleware = composeWithDevTools({ trace: true, traceLimit: 25 })(middleware);
    }

    const rootReducer = combineReducers({
        ...reducers,
        routing: routerReducer
    });

    return createStore(
        rootReducer,
        initialState,
        middleware) as Store<ApplicationState>;
}

export type ApplicationState = Readonly<{
    comicbookList: ComicbookListState,
    signUp: CreateUserState,
    commonUi: CommonUiState,
    router: RouterState,
    confirmEmail: ConfirmEmailState,
}>
