import { combineReducers } from 'redux';

import adminUser from './adminUser';
import application from './application';
import audit from './audit';
import auth from './auth';
import company from './company';
import consumptionProfile from './consumptionProfile';
import dashboard from './dashboard';
import device from './device';
import deviceInfo from './deviceInfo';
import deviceUser from './deviceUser';
import document from './document';
import generalConfig from './generalConfig';
import geofence from './geofence';
import group from './group';
import location from './location';
import message from './message';
import plan from './plan';
import qrcode from './qrcode';
import reportDate from './reportDate';
import reportGps from './reportGps';
import reports from './reports';
import site from './site';
import subgroup from './subgroup';


const reducers = () =>
  combineReducers({
    adminUser,
    application,
    audit,
    auth,
    company,
    consumptionProfile,
    dashboard,
    device,
    deviceInfo,
    deviceUser,
    document,
    generalConfig,
    geofence,
    group,
    location,
    message,
    plan,
    qrcode,
    reportDate,
    reportGps,
    reports,
    site,
    subgroup,
  });

export default reducers;
