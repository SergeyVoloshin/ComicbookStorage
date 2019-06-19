import React, { Component } from "react";
import { bindActionCreators, Dispatch } from "redux";
import { connect } from "react-redux";
import { History } from "history";
import { logOut } from "../store/logIn/actions";
import { ApplicationState } from "../store/configureStore";
import AppPathConfig from "../utils/appPathConfig";
import { LogOutReason } from "../store/logIn/types"

type LogOutProps = ReturnType<typeof mapStateToProps> &
    ReturnType<typeof mapDispatchToProps> &
{
    history: History
}

export class LogOut extends Component<LogOutProps> {

    componentDidMount() {
        const { logOut, history } = this.props;
        logOut(LogOutReason.UserRequest);
        history.push(AppPathConfig.home);
    }

    render() {
        return ('');
    }
}

const mapStateToProps = (state: ApplicationState) => {
    return { logInState: state.logIn }
}

const mapDispatchToProps = (dispatch: Dispatch) => bindActionCreators(
    {
        logOut: logOut,
    },
    dispatch
);


export default connect(mapStateToProps, mapDispatchToProps)(LogOut)