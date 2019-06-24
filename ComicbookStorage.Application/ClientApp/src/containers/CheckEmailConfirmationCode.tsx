import React, { Component } from 'react';
import { bindActionCreators, Dispatch } from "redux";
import { connect } from 'react-redux';
import { History } from 'history';
import { match } from "react-router-dom";
import { ApplicationState } from '../store/configureStore';
import { confirmEmailAsync } from '../store/emailConfirmationCode/thunks';
import ErrorMessage from "../components/ErrorMessage";

type EmailConfirmationProps = ReturnType<typeof mapStateToProps> &
    ReturnType<typeof mapDispatchToProps> &
{
    history: History,
    match: match<RouteParams>,
}

interface RouteParams { code: string; }

export class CheckEmailConfirmationCode extends Component<EmailConfirmationProps> {

    componentDidMount() {
        const { confirmEmail, match, history } = this.props;
        confirmEmail(match.params.code, history);
    }

    render() {
        const { confirmationState } = this.props;

        if (confirmationState.errors) {
            return (<ErrorMessage errors={confirmationState.errors} />);
        }
        return (<div>Confirming the email...</div>);
    }

}

const mapStateToProps = (state: ApplicationState) => {
    return { confirmationState: state.confirmEmail, }
}

const mapDispatchToProps = (dispatch: Dispatch) => bindActionCreators(
    {
        confirmEmail: confirmEmailAsync,
    },
    dispatch
);

export default connect(mapStateToProps, mapDispatchToProps)(CheckEmailConfirmationCode);