import React, { Component } from "react";
import { bindActionCreators, Dispatch } from "redux";
import { connect } from "react-redux";
import { InjectedFormProps, Field, reduxForm, WrappedFieldProps, FormErrors } from "redux-form";
import { Form, Button, Col, Row } from "reactstrap";
import { History } from 'history';
import InputField, { InputProps } from "../components/InputField";
import { ApplicationState } from "../store/configureStore";
import { isUniqueFieldTakenAsync } from "../store/signUp/thunks";
import { requestInitialUserDataAsync, updateUserAsync } from "../store/accountInfo/thunks";
import { UpdatedUser } from "../store/accountInfo/types";
import { required, email, minLength, maxLength } from "../utils/validators";
import ErrorMessage from "../components/ErrorMessage";

type AccountInfoProps = ReturnType<typeof mapStateToProps> &
    ReturnType<typeof mapDispatchToProps> &
{
    history: History
}

const emailField: string = "email";
const nameField: string = "name";

export class AccountInfo extends Component<InjectedFormProps<UpdatedUser, AccountInfoProps> & AccountInfoProps> {

    componentDidMount() {
        this.props.getInitialUserData();
    }

    submit = (values: UpdatedUser) => {
        this.props.updateUser(values, this.props.history);
    }

    maxLength255 = maxLength(255);
    maxLength50 = maxLength(50);
    minLength5 = minLength(5);

    renderField = (fieldProperties: WrappedFieldProps & InputProps): JSX.Element => {
        return (<InputField {...fieldProperties} />);
    }

    render() {
        const { handleSubmit, submitting, accountInfoState } = this.props;

        return (
            <div>
                <ErrorMessage errors={accountInfoState.errors} />
                <Form onSubmit={handleSubmit(this.submit)}>
                    <Row form>
                        <Col md={12}>
                            <h1>User Info</h1>
                        </Col>                        
                    </Row>
                    <Field
                        name={emailField}
                        id={emailField}
                        type="email"
                        placeholder="user@example.com"
                        component={this.renderField}
                        label="Email"
                        formText="If you change your email we will log you off and ask to confirm it"
                        validate={[this.maxLength255, email]} />
                    <Field
                        name={nameField}
                        id={nameField}
                        type="text"
                        placeholder="John Doe"
                        component={this.renderField}
                        label="Display Name"
                        validate={[this.minLength5, this.maxLength50]} />
                    <br />
                    <Row form>
                        <Col md={12}>
                            <h1>Password</h1>
                        </Col>
                    </Row>
                    <Field
                        name="oldPassword"
                        id="oldPassword"
                        type="password"
                        component={this.renderField}
                        label="Old Password" />
                    <Field
                        name="password"
                        id="password"
                        type="password"
                        component={this.renderField}
                        label="New Password"
                        validate={[this.minLength5, this.maxLength255]} />
                    <Field
                        name="confirmPassword"
                        id="confirmPassword"
                        type="password"
                        component={this.renderField}
                        label="Confirm Password" />
                    <Row form>
                        <Col md={8}>
                            <Button type="submit" disabled={submitting} >Update</Button>
                        </Col>
                    </Row>
                </Form >
            </div>
        );
    }
}

const formValidate = (values: UpdatedUser, props: AccountInfoProps & InjectedFormProps<UpdatedUser, AccountInfoProps>): FormErrors<UpdatedUser> => {
    let errors: FormErrors<UpdatedUser> = {};
    const requiredError = "This field is required to change the password";
    if (values.password || values.confirmPassword || values.oldPassword) {
        if (!values.password) {
            errors.password = requiredError;
        }
        if (!values.confirmPassword) {
            errors.confirmPassword = requiredError;
        }
        if (!values.oldPassword) {
            errors.oldPassword = requiredError;
        }
    }
    if (values.password !== values.confirmPassword) {
        errors.confirmPassword = "The new password and its confirmation do not match";
    }
    return errors;
}

const asyncFormValidate = (
    values: UpdatedUser,
    dispatch: Dispatch,
    props: AccountInfoProps & InjectedFormProps<UpdatedUser, AccountInfoProps> & { asyncErrors: FormErrors<UpdatedUser> },
    blurredField: keyof UpdatedUser): Promise<any> => {
    if (blurredField && values[blurredField]) {
        return isUniqueFieldTakenAsync(blurredField, values[blurredField], props.asyncErrors);
    } else {
        return new Promise<boolean>(resolve => resolve());
    }

}

const mapStateToProps = (state: ApplicationState) => {
    return {
        accountInfoState: state.accountInfo,
        initialValues: state.accountInfo.userData
    }
}

const mapDispatchToProps = (dispatch: Dispatch) => bindActionCreators(
    {
        getInitialUserData: requestInitialUserDataAsync,
        updateUser: updateUserAsync,
    },
    dispatch
);

const accountInfoForm = reduxForm<UpdatedUser, AccountInfoProps>({
    form: "accountInfo",
    validate: formValidate,
    asyncValidate: asyncFormValidate,
    asyncBlurFields: [nameField, emailField],
    enableReinitialize: true
})(AccountInfo);

export default connect(mapStateToProps, mapDispatchToProps)(accountInfoForm)
