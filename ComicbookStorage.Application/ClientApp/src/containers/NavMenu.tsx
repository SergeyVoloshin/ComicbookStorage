import React, { Component } from 'react';
import { bindActionCreators, Dispatch } from 'redux';
import { connect } from 'react-redux';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, Nav, UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import { Link } from 'react-router-dom';
import 'bootstrap/dist/js/bootstrap.js';
import './NavMenu.css';
import AppPathConfig from "../utils/appPathConfig";
import { ApplicationState } from '../store/configureStore';
import { toggleMenu } from '../store/navMenu/actions';

type NavMenuProps = ReturnType<typeof mapStateToProps> &
    ReturnType<typeof mapDispatchToProps>

export class NavMenu extends Component<NavMenuProps> {
    getAccountLinks = () => {
        if (this.props.logInState.authenticated) {
            return [
                <Link key={3} className="dropdown-item" to={AppPathConfig.accountInfo}>Settings</Link>,
                <Link key={2} className="dropdown-item" to={AppPathConfig.logOut}>Log Out</Link>
            ];
        }
        return [
            <Link key={0} className="dropdown-item" to={AppPathConfig.logIn}>Log In</Link>,
            <Link key={1} className="dropdown-item" to={AppPathConfig.signUp}>Sign Up</Link>
        ];
    }

    render() {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" light >
                    <Container>
                        <NavbarBrand tag={Link} to="/">ComicbookStorage.Application</NavbarBrand>
                        <NavbarToggler onClick={this.props.toggleMenu} className="mr-2"  />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={this.props.navMenuState.isOpen} navbar>
                            <Nav className="ml-auto" navbar>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to={AppPathConfig.home}>Home</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to={AppPathConfig.comicbooks}>Comicbooks</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to={AppPathConfig.addComicbook}>Add Comicbook</NavLink>
                                </NavItem>
                                <li className="nav-item dropdown">
                                    <a className="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        Account
                                    </a>
                                    <div className="dropdown-menu" aria-labelledby="navbarDropdown">
                                        {this.getAccountLinks()}
                                    </div>
                                </li>
                            </Nav>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}

const mapStateToProps = (state: ApplicationState) => {
    return {
        navMenuState: state.navMenu,
        logInState: state.logIn,
    }
}

const mapDispatchToProps = (dispatch: Dispatch) => bindActionCreators(
    {
        toggleMenu: toggleMenu,
    },
    dispatch
);

export default connect(mapStateToProps, mapDispatchToProps)(NavMenu)
