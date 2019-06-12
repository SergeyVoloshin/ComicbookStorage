import fetch from 'cross-fetch';
import { store } from '..';
import { setProgressBar } from '../store/commonUi/actions';
import { processLoggedIn } from '../store/logIn/actions';
import messageBox from "./messageBox";


class ComicbookServer {
    get<T>(url: string, sendAuthenticationToken: boolean, showProgressbar: boolean): Promise<T> {
        checkProgressStart(showProgressbar);

        let request: RequestInit = {
            method: "GET"
        }
        if (sendAuthenticationToken) {
            request = {
                ...request,
                headers: getAuthenticationHeader()
            };
        }
        return fetch(url, request)
            .then(
                response => {
                    checkProgressStop(showProgressbar);
                    if (!response.ok) {
                        messageBox.showGeneralError();
                    }
                    return response;
                })
            .then(
                response => {
                    return response.json();
                },
                error => {
                    messageBox.showGeneralError();
                    return error;
                }
            );
    }

    post<T>(url: string, data: T, sendAuthenticationToken: boolean, showProgressbar: boolean): Promise<Response> {
        return sendJson(url, data, "POST", sendAuthenticationToken, showProgressbar);
    }

    put<T>(url: string, data: T, sendAuthenticationToken: boolean, showProgressbar: boolean): Promise<Response> {
        return sendJson(url, data, "PUT", sendAuthenticationToken, showProgressbar);
    }

    setAuthenticationToken(token: string) {
        localStorage.setItem(authenticationTokenKey, token);
    }

    clearAuthenticationToken() {
        localStorage.removeItem(authenticationTokenKey);
    }

    updateAuthenticationState() {
        if (localStorage.getItem(authenticationTokenKey)) {
            store.dispatch(processLoggedIn());
        }
    }
    
}

const getAuthenticationHeader = () => {
    let token: string | null = localStorage.getItem(authenticationTokenKey);
    if (token) {
        return { "Authorization": `Bearer ${token}` }
    }
    const errorMessage = "You are not authorized to perform this operation";
    messageBox.showError(errorMessage);
    throw errorMessage;
}

const authenticationTokenKey: string = "authenticationToken";

const sendJson = <T>(url: string, data: T, method: string, sendAuthenticationToken: boolean, showProgressbar: boolean): Promise<Response> => {
    checkProgressStart(showProgressbar);
    let headers: HeadersInit = {
        "Accept": "application/json",
        "Content-Type": "application/json"
    }
    if (sendAuthenticationToken) {
        headers = {
            ...headers,
            ...getAuthenticationHeader()
        }
    }        
    return fetch(url,
        {
            method: method,
            headers: headers,
            body: data ? JSON.stringify(data) : null,
        })
        .then(
            response => {
                checkProgressStop(showProgressbar);
                return response;
            }
            ,
            error => {
                messageBox.showGeneralError();
                return error;
            }
        );
}

const checkProgressStart = (showProgressbar: boolean) => {
    if (showProgressbar) {
        store.dispatch(setProgressBar(true));
    }
}

const checkProgressStop = (showProgressbar: boolean) => {
    if (showProgressbar) {
        store.dispatch(setProgressBar(false));
    }
}

const comicbookServer = new ComicbookServer();

export default comicbookServer