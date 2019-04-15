import { applyMiddleware, combineReducers, compose, createStore, Store } from 'redux';
import thunk from 'redux-thunk';
import { routerReducer, routerMiddleware, RouterState } from 'react-router-redux';
import { composeWithDevTools } from 'redux-devtools-extension';
import { History } from 'history';
import * as Counter from './Counter';
import * as WeatherForecasts from './WeatherForecasts';
import { comicbookListReducer } from './comicbookList/reducer';
import { progressBarReducer } from './commonUi/reducer';
import { ComicbookListState } from './comicbookList/types';
import { CommonUiState } from './commonUi/types';

export default function configureStore(history: History, initialState: ApplicationState): Store<ApplicationState> {
    const reducers = {
        counter: Counter.reducer,
        weatherForecasts: WeatherForecasts.reducer,
        comicbookList: comicbookListReducer,
        commonUi: progressBarReducer
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
    router: RouterState,
    commonUi: CommonUiState,
}>
