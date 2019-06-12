import React, { Component } from "react";
import { bindActionCreators, Dispatch } from "redux";
import { connect } from 'react-redux';
import { InjectedFormProps, Field, reduxForm, WrappedFieldProps, FormErrors } from "redux-form";
import { Form, Button, Col, Row } from "reactstrap";
import { History } from 'history';
import InputField, { InputProps } from "../components/InputField";
import { ApplicationState } from "../store/configureStore";
import { logInAsync } from "../store/logIn/thunks";
import { LogInRequest } from "../store/logIn/types";
import { required, email } from "../utils/validators";
import ErrorMessage from "../components/ErrorMessage";

type LogInProps = ReturnType<typeof mapStateToProps> &
    ReturnType<typeof mapDispatchToProps> &
    {
        history: History
    }

export class LogIn extends Component<InjectedFormProps<LogInRequest, LogInProps> & LogInProps> {

    submit = (values: LogInRequest) => {
        this.props.logIn(values, this.props.history);
    }

    renderField = (fieldProperties: WrappedFieldProps & InputProps): JSX.Element => {
        return (<InputField {...fieldProperties} />);
    }

    render() {
        const { handleSubmit, submitting, logInState } = this.props;

        return (
            <div>
                <ErrorMessage errors={logInState.errors} />
                <Form onSubmit={handleSubmit(this.submit)}>
                    <Field
                        name="email"
                        id="email"
                        type="email"
                        placeholder="user@example.com"
                        component={this.renderField}
                        label="Email*"
                        validate={[required, email]} />
                    <Field
                        name="password"
                        id="password"
                        type="password"
                        component={this.renderField}
                        label="Password*"
                        validate={[required]} />
                    <Row form>
                        <Col md={8}>
                            <Button type="submit" disabled={submitting} >Log In</Button>
                        </Col>
                    </Row>
                </Form >
            </div>
        );
    }
}

const mapStateToProps = (state: ApplicationState) => {
    return { logInState: state.logIn }
}

const mapDispatchToProps = (dispatch: Dispatch) => bindActionCreators(
    {
        logIn: logInAsync,
    },
    dispatch
);

const logInForm = reduxForm<LogInRequest, LogInProps>({
    form: "logIn",
})(LogIn);

export default connect(mapStateToProps, mapDispatchToProps)(logInForm)
