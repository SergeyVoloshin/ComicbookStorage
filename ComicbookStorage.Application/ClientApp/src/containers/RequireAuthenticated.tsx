import React, { Component } from 'react';
import { connect } from 'react-redux';
import { History } from 'history';
import { ApplicationState } from '../store/configureStore';
import AppPathConfig from "../utils/appPathConfig";

type RequireAuthenticatedProps = ReturnType<typeof mapStateToProps> &
{
    history: History
}

export default function (ComposedComponent: typeof React.Component) {
    class RequireAuthenticated extends Component<RequireAuthenticatedProps> {

        componentDidMount() {
            this.redirectIfNotAuthenticated();
        }

        componentDidUpdate() {
            this.redirectIfNotAuthenticated();
        }

        redirectIfNotAuthenticated() {
            if (!this.props.authenticated) {
                this.props.history.push(AppPathConfig.logIn);
            }
        }

        render() {
            if (!this.props.authenticated) {
                return '';
            }
            return <ComposedComponent {...this.props} />;
        }
    }

    return connect(mapStateToProps)(RequireAuthenticated);
}

const mapStateToProps = (state: ApplicationState) => {

    return { authenticated: state.logIn.authenticated };
}