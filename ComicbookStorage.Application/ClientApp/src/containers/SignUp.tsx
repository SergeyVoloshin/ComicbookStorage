import React, { Component } from "react";
import { bindActionCreators, Dispatch } from "redux";
import { connect } from "react-redux";
import { InjectedFormProps, Field, reduxForm, WrappedFieldProps, FormErrors } from "redux-form";
import { Form, FormGroup, Label, Button, FormText, Col, Input, FormFeedback, Spinner, Row, InputGroup, InputGroupAddon } from "reactstrap";
import { ApplicationState } from "../store/configureStore";
import { createUserAsync, isUniqueFieldTakenAsync } from "../store/signUp/thunks";
import { User } from "../store/signUp/types";
import { required, email, minLength, maxLength } from "../utils/validators";

type SignUpProps = ReturnType<typeof mapStateToProps> &
    ReturnType<typeof mapDispatchToProps>

interface InputProps {
    id: string,
    placeholder?: string,
    type: string,
    formText?: string,
}

const emailField: string = "email";
const nameField: string = "name";

export class SignUp extends Component<InjectedFormProps<User, SignUpProps> & SignUpProps> {

    submit = (values: User) => {
        this.props.createUser();
    }

    maxLength255 = maxLength(255);
    maxLength50 = maxLength(50);
    minLength5 = minLength(5);

    renderField = (fieldProperties: WrappedFieldProps & InputProps): JSX.Element => {
        let displayError = fieldProperties.meta.touched && typeof fieldProperties.meta.error !== 'undefined';
        return (
            <Row form>
                <Col md={8}>
                    <FormGroup>
                        <Label for={fieldProperties.id}>{fieldProperties.label}</Label>
                        <InputGroup className="flex-wrap">
                            <Input
                                {...fieldProperties.input}
                                id={fieldProperties.id}
                                type={fieldProperties.type}
                                placeholder={fieldProperties.placeholder}
                                invalid={displayError} />
                            {fieldProperties.meta.asyncValidating && <InputGroupAddon addonType="append" className="d-inline-block align-middle mt-1 ml-n4">
                                <Spinner size="sm" type="grow" color="primary" />
                            </InputGroupAddon>}
                            {displayError && <FormFeedback className="w-100">{fieldProperties.meta.error}</FormFeedback>}
                        </InputGroup>
                        {typeof fieldProperties.formText !== "undefined" && <FormText color="muted">{fieldProperties.formText}</FormText>}
                    </FormGroup>
                </Col>
            </Row>
        );
    }

    render() {
        const { handleSubmit, submitting } = this.props;
        return (
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
        );
    }
}

const formValidate = (values: User, props: SignUpProps & InjectedFormProps<User, SignUpProps>): FormErrors<User> => {
    let errors: FormErrors<User> = {};
    if (values.password !== values.confirmPassword) {
        errors.confirmPassword = "Your password and confirmation password do not match";
    }
    return errors;
}

const asyncFormValidate = (values: User,
    dispatch: Dispatch,
    props: SignUpProps & InjectedFormProps<User, SignUpProps> & { asyncErrors: FormErrors<User> },
    blurredField: keyof User): Promise<any> => {
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

const signUpForm = reduxForm<User, SignUpProps>({
    form: "signUp",
    validate: formValidate,
    asyncValidate: asyncFormValidate,
    asyncBlurFields: [nameField, emailField]
})(SignUp);

export default connect(mapStateToProps, mapDispatchToProps)(signUpForm)
