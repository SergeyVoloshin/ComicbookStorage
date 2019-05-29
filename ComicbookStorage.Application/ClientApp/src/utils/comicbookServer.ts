import fetch from 'cross-fetch';
import { store } from '..';
import { setProgressBar } from '../store/commonUi/actions';
import messageBox from "./messageBox";


class ComicbookServer {
    get<T>(url: string, showProgressbar: boolean = true): Promise<T> {
        checkProgressStart(showProgressbar);

        return fetch(url)
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

    post<T>(url: string, data: T, showProgressbar: boolean = true): Promise<Response> {
        return sendJson(url, data, "POST", showProgressbar);
    }

    put<T>(url: string, data: T, showProgressbar: boolean = true): Promise<Response> {
        return sendJson(url, data, "PUT", showProgressbar);
    }
}

const sendJson = <T>(url: string, data: T, method: string, showProgressbar: boolean): Promise<Response> => {
    checkProgressStart(showProgressbar);

    return fetch(url,
        {
            method: method,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
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