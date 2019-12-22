import React from 'react';
import { Route } from 'react-router';
import { NotificationContainer } from 'react-notifications';
import 'react-notifications/lib/notifications.css';
import Layout from './containers/Layout';
import Home from './components/Home';
import ComicbookList from './containers/ComicbookList';
import ProgressBar from './containers/ProgressBar';
import CheckEmailConfirmationCode from './containers/CheckEmailConfirmationCode';
import LogIn from './containers/LogIn';
import LogOut from './containers/LogOut';
import SignUp from './containers/SignUp';
import AddComicbook from './containers/AddComicbook';
import ConfirmEmail from './containers/ConfirmEmail';
import RequireAuthenticated from './containers/RequireAuthenticated';
import RequireUnauthenticated from './containers/RequireUnauthenticated';
import RestoreAccess from "./containers/RestoreAccess";
import AccountInfo from "./containers/AccountInfo";
import AppPathConfig from './utils/appPathConfig';

export default () => (
    <div>
        <ProgressBar />
        <NotificationContainer />
        <Layout>
            <Route exact path={AppPathConfig.home} component={Home} />
            <Route path={AppPathConfig.comicbooks} component={ComicbookList} />
            <Route path={AppPathConfig.confirmEmail} component={RequireUnauthenticated(ConfirmEmail)} />
            <Route path={AppPathConfig.sendEmailConfirmationCode} component={CheckEmailConfirmationCode} />
            <Route path={AppPathConfig.signUp} component={RequireUnauthenticated(SignUp)} />
            <Route path={AppPathConfig.logIn} component={RequireUnauthenticated(LogIn)} />
            <Route path={AppPathConfig.logOut} component={RequireAuthenticated(LogOut)} />
            <Route path={AppPathConfig.addComicbook} component={RequireAuthenticated(AddComicbook)} />
            <Route path={AppPathConfig.restoreAccess} component={RequireUnauthenticated(RestoreAccess)} />
            <Route path={AppPathConfig.accountInfo} component={RequireAuthenticated(AccountInfo)} />
        </Layout>
    </div>
);
