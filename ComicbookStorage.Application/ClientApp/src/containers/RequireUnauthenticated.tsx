import React, { Component } from 'react';
import { connect } from 'react-redux';
import { History } from 'history';
import { ApplicationState } from '../store/configureStore';
import AppPathConfig from "../utils/appPathConfig";

type RequireUnauthenticatedProps = ReturnType<typeof mapStateToProps> &
{
    history: History
}

export default function (ComposedComponent: typeof React.Component) {
    class RequireUnauthenticated extends Component<RequireUnauthenticatedProps> {

        componentDidMount() {
            this.redirectIfAuthenticated();
        }

        componentDidUpdate() {
            this.redirectIfAuthenticated();
        }

        redirectIfAuthenticated() {
            if (this.props.authenticated) {
                this.props.history.push(AppPathConfig.home);
            }
        }

        render() {
            if (this.props.authenticated) {
                return '';
            }
            return <ComposedComponent {...this.props} />;
        }
    }

    return connect(mapStateToProps)(RequireUnauthenticated);
}

const mapStateToProps = (state: ApplicationState) => {

    return { authenticated: state.logIn.authenticated };
}