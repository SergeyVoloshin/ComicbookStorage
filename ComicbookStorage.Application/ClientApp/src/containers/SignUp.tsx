import React, { Component } from "react";
import { bindActionCreators, Dispatch } from "redux";
import { connect } from "react-redux";
import { InjectedFormProps, Field, reduxForm, WrappedFieldProps, FormErrors } from "redux-form";
import { Form, FormGroup, Label, Button, FormText, Col, Input, FormFeedback, Spinner, Row } from 'reactstrap';
import { ApplicationState } from "../store/configureStore";
import { createUserAsync } from "../store/signUp/thunks";
import { User } from "../store/signUp/types";
import { required, email, minLength } from "../utils/validators";

type SignUpProps = ReturnType<typeof mapStateToProps> &
    ReturnType<typeof mapDispatchToProps>

interface InputProps {
    id: string,
    placeholder?: string,
    type: string,
    formText?: string,
}

export class SignUp extends Component<InjectedFormProps<User, SignUpProps> & SignUpProps> {

    minLength5 = minLength(5);

    submit = (values: User) => {
        this.props.createUser();
    }

    renderField = (fieldProperties: WrappedFieldProps & InputProps): JSX.Element => {
        let displayError: boolean = fieldProperties.meta.touched && typeof fieldProperties.meta.error !== 'undefined';
        return (
            <Row form>
                <Col md={8}>
                    <FormGroup>
                        <Label for={fieldProperties.id}>{fieldProperties.label}</Label>
                        <Input
                            {...fieldProperties.input}
                            id={fieldProperties.id}
                            type={fieldProperties.type}
                            placeholder={fieldProperties.placeholder}
                            invalid={displayError} />
                        {fieldProperties.meta.asyncValidating && <Spinner size="sm" type="grow" color="primary" />}
                        {displayError && <FormFeedback>{fieldProperties.meta.error}</FormFeedback>}
                        {typeof fieldProperties.formText !== 'undefined' && <FormText color="muted">{fieldProperties.formText}</FormText>}
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
                    name="email"
                    id="email"
                    type="email"
                    placeholder="user@example.com"
                    component={this.renderField}
                    label="Email*"
                    formText="We'll never share your email with anyone else."
                    validate={[required, email]} />
                <Field
                    name="name"
                    id="name"
                    type="text"
                    placeholder="John Doe"
                    component={this.renderField}
                    label="Display Name*"
                    validate={[required, this.minLength5]} />
                <Field
                    name="password"
                    id="password"
                    type="password"
                    component={this.renderField}
                    label="Password*"
                    validate={[required, this.minLength5]} />
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

const sleep = (ms: number) => new Promise(resolve => setTimeout(resolve, ms));
const asyncFormValidate = (values: User,
    dispatch: Dispatch,
    props: SignUpProps & InjectedFormProps<User, SignUpProps>,
    blurredField: string): Promise<any> => {
    console.log('sdfsdfsdf');
    return sleep(1000).then(() => {
        // simulate server latency
        if (['john', 'paul', 'george', 'ringo'].includes(values.name)) {
            console.log('sdfsdfsdf1111');
            throw { name: 'That username is taken' }
        }
    })
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
    asyncBlurFields: ['name']
})(SignUp);

export default connect(mapStateToProps, mapDispatchToProps)(signUpForm)
