import React, { Component } from 'react';


import CodeCampMenu from './CodeCampMenu';
import PageTop from './PageTop';
import Footer from './Footer';
import Routes from "../../Routes";
import Header from "./Header";

class FullPage extends Component {

    constructor(props){
        super(props);
        this.handler = this.handler.bind(this);
    }

    handler(val) {
        this.props.action();
    }


    render() {
        return (
            <div class="wrapper">
                <Header/>
                <Routes  action={this.handler}  />
                <Footer />
            </div>
        );
    }
}

FullPage.defaultProps = {};

export default FullPage;
