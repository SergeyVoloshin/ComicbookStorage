import React, { Component } from "react";
import { bindActionCreators, Dispatch } from "redux";
import { connect } from "react-redux";
import { InjectedFormProps, Field, reduxForm, WrappedFieldProps, FormErrors } from "redux-form";
import { Form, Button, Col, Row } from "reactstrap";
import { History } from 'history';
import InputField, { InputProps } from "../components/InputField";
import { ApplicationState } from "../store/configureStore";
import { createUserAsync, isUniqueFieldTakenAsync } from "../store/signUp/thunks";
import { CreatedUser } from "../store/signUp/types";
import { required, email, minLength, maxLength } from "../utils/validators";
import ErrorMessage from "../components/ErrorMessage";

type SignUpProps = ReturnType<typeof mapStateToProps> &
    ReturnType<typeof mapDispatchToProps> &
{
    history: History
}

const emailField: string = "email";
const nameField: string = "name";

export class SignUp extends Component<InjectedFormProps<CreatedUser, SignUpProps> & SignUpProps> {

    submit = (values: CreatedUser) => {
        this.props.createUser(values, this.props.history);
    }

    maxLength255 = maxLength(255);
    maxLength50 = maxLength(50);
    minLength5 = minLength(5);

    renderField = (fieldProperties: WrappedFieldProps & InputProps): JSX.Element => {
        return (<InputField {...fieldProperties} />);
    }

    render() {
        const { handleSubmit, submitting, signUpState } = this.props;

        return (
            <div>
                <ErrorMessage errors={signUpState.errors} />
                <Form onSubmit={handleSubmit(this.submit)}>
                    <Field
                        name={emailField}
                        id={emailField}
                        type="email"
                        placeholder="user@example.com"
                        component={this.renderField}
                        label="Email*"
                        formText="We'll never share your email with anyone else."
                        validate={[required, this.maxLength255, email]} />
                    <Field
                        name={nameField}
                        id={nameField}
                        type="text"
                        placeholder="John Doe"
                        component={this.renderField}
                        label="Display Name*"
                        validate={[required, this.minLength5, this.maxLength50]} />
                    <Field
                        name="password"
                        id="password"
                        type="password"
                        component={this.renderField}
                        label="Password*"
                        validate={[required, this.minLength5, this.maxLength255]} />
                    <Field
                        name="confirmPassword"
                        id="confirmPassword"
                        type="password"
                        component={this.renderField}
                        label="Confirm Password*"
                        validate={required} />
                    <Row form>
                        <Col md={8}>
                            <Button type="submit" disabled={submitting} >Sign Up</Button>
                        </Col>
                    </Row>
                </Form >
            </div>
        );
    }
}

const formValidate = (values: CreatedUser, props: SignUpProps & InjectedFormProps<CreatedUser, SignUpProps>): FormErrors<CreatedUser> => {
    let errors: FormErrors<CreatedUser> = {};
    if (values.password !== values.confirmPassword) {
        errors.confirmPassword = "Your password and confirmation password do not match";
    }
    return errors;
}

const asyncFormValidate = (
    values: CreatedUser,
    dispatch: Dispatch,
    props: SignUpProps & InjectedFormProps<CreatedUser, SignUpProps> & { asyncErrors: FormErrors<CreatedUser> },
    blurredField: keyof CreatedUser): Promise<any> => {
    if (blurredField) {
        return isUniqueFieldTakenAsync(blurredField, values[blurredField], props.asyncErrors);
    } else {
        return new Promise<boolean>(resolve => resolve());
    }
}

const mapStateToProps = (state: ApplicationState) => {
    return { signUpState: state.signUp }
}

const mapDispatchToProps = (dispatch: Dispatch) => bindActionCreators(
    {
        createUser: createUserAsync,
    },
    dispatch
);

const signUpForm = reduxForm<CreatedUser, SignUpProps>({
    form: "signUp",
    validate: formValidate,
    asyncValidate: asyncFormValidate,
    asyncBlurFields: [nameField, emailField]
})(SignUp);

export default connect(mapStateToProps, mapDispatchToProps)(signUpForm)
