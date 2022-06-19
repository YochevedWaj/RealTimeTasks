import React from 'react';
import { Route } from 'react-router';
import Layout from './Components/Layout';
import Home from './Pages/Home';
import Login from './Pages/Login';
import Logout from './Pages/Logout';
import Signup from './Pages/Signup';
import { AuthContextComponent } from './AuthContext';
import PrivateRoute from './Components/PrivateRoute';


export default function App() {
    return (
        <AuthContextComponent>
            <Layout>
                <PrivateRoute exact path='/' component={Home} />
                <Route exact path='/login' component={Login} />
                <Route exact path='/signup' component={Signup} />
                <PrivateRoute exact path='/logout' component={Logout} />
            </Layout>
        </AuthContextComponent>
    )
}