import React, { Component } from "react";
import { bindActionCreators, Dispatch } from "redux";
import { connect } from "react-redux";
import { InjectedFormProps, Field, reduxForm, WrappedFieldProps } from "redux-form";
import { Form, FormGroup, Label, Button, FormText, Col, Input } from 'reactstrap';
import { ApplicationState } from "../store/configureStore";
import { createUserAsync } from "../store/signUp/thunks";
import { User } from "../store/signUp/types";

type SignUpProps = ReturnType<typeof mapStateToProps> &
    ReturnType<typeof mapDispatchToProps>

interface InputProps {
    id: string,
    placeholder?: string,
    type: string,
    formText?: string,
}

export class SignUp extends Component<InjectedFormProps<User, SignUpProps> & SignUpProps> {


    renderField = (fieldProperties: WrappedFieldProps & InputProps): JSX.Element => {
        return (
            <Col md={8}>
                <FormGroup>
                    <Label for={fieldProperties.id}>{fieldProperties.label}</Label>
                    <Input {...fieldProperties.input} id={fieldProperties.id} type={fieldProperties.type} placeholder={fieldProperties.placeholder} />
                    {typeof fieldProperties.formText !== 'undefined' && <FormText color="muted">{fieldProperties.formText}</FormText>}
                </FormGroup>
            </Col>
        );
    }

    render() {
        const { createUser } = this.props;
        return (
            <Form onSubmit={createUser}>
                <Field name="email" id="email" type="email" placeholder="user@example.com" component={this.renderField} label="Email" formText="We'll never share your email with anyone else." />
                <Field name="name" id="name" type="text" placeholder="John Doe" component={this.renderField} label="Display Name" />
                <Field name="password" id="password" type="password" component={this.renderField} label="Password" />
                <Col md={8}>
                    <Button type="submit">Sign Up</Button>
                </Col>
            </Form >
        );
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
    form: "signUp"
})(SignUp);

export default connect(mapStateToProps, mapDispatchToProps)(signUpForm)
