import React from 'react';
import {
  Switch,
  Route,
  Redirect,
  RouteProps,
  useRouteMatch,
} from 'react-router-dom';

import routes from '.';
import Layout from '../components/Layout';
import { usePrivilege } from '../hooks/usePrivilege';
import { useAppSelector } from '../hooks/useRedux';
import EditAdminUser from '../pages/AdminUsers/EditAdminUser';
import ManageAdminUsers from '../pages/AdminUsers/ManageAdminUsers';
import ApplicationHistory from '../pages/Android/Application/ApplicationHistory';
import ApplicationManage from '../pages/Android/Application/ApplicationManage';
import ApplicationRegister from '../pages/Android/Application/ApplicationRegister';
import ApplicationSendStatus from '../pages/Android/Application/ApplicationSendStatus';
import BlockApplication from '../pages/Android/BlockApplication';
import BlockApplicationGeneralEdit from '../pages/Android/BlockApplication/GeneralCard/BlockApplicationGeneralEdit';
import BlockApplicationGroupEdit from '../pages/Android/BlockApplication/GroupCard/BlockApplicationGroupEdit';
import BlockApplicationSubgroupEdit from '../pages/Android/BlockApplication/SubgroupCard/BlockApplicationSubgroupEdit';
import BlockApplicationUserEdit from '../pages/Android/BlockApplication/UserCard/BlockApplicationUserEdit';
import BlockSiteCategory from '../pages/Android/BlockSiteCategory';
import BlockSiteUrl from '../pages/Android/BlockSiteUrl';
import BlockSiteUrlGeneralEdit from '../pages/Android/BlockSiteUrl/GeneralCard/BlockSiteUrlGeneralEdit';
import BlockSiteUrlGroupEdit from '../pages/Android/BlockSiteUrl/GroupCard/BlockSiteUrlGroupEdit';
import BlockSiteUrlSubgroupEdit from '../pages/Android/BlockSiteUrl/SubgroupCard/BlockSiteUrlSubgroupEdit';
import BlockSiteUrlUserEdit from '../pages/Android/BlockSiteUrl/UserCard/BlockSiteUrlUserEdit';
import KioskMode from '../pages/Android/KioskMode';
import ApplicationDeviceUsers from '../pages/Applications/ApplicationDeviceUsers';
import ConsumptionHistory from '../pages/Applications/ConsumptionHistory';
import ManageApplications from '../pages/Applications/ManageApplications';
import Audit from '../pages/Audit';
import CompanyConsumption from '../pages/Companies/CompanyConsumption';
import CompanyInfo from '../pages/Companies/CompanyInfo';
import CompanyLicense from '../pages/Companies/CompanyLicense';
import ConsumptionProfile from '../pages/Configuration/ConsumptionProfile';
import EditGroupConsumptionRole from '../pages/Configuration/ConsumptionProfile/EditGroupConsumptionRole';
import EditSubgroupConsumptionRole from '../pages/Configuration/ConsumptionProfile/EditSubgroupConsumptionRole';
import EditUserConsumptionRole from '../pages/Configuration/ConsumptionProfile/EditUserConsumptionRole';
import GeneralConfig from '../pages/Configuration/GeneralConfig';
import GeneralConfigGroupEdit from '../pages/Configuration/GeneralConfig/GeneralConfigGroupEdit';
import GeneralConfigSubgroupEdit from '../pages/Configuration/GeneralConfig/GeneralConfigSubgroupEdit';
import GeneralConfigUserEdit from '../pages/Configuration/GeneralConfig/GeneralConfigUserEdit';
import QRCode from '../pages/Configuration/QRCode/index';
import QRCodeDeviceOwner from '../pages/Configuration/QRCode/QRCodeDeviceOwner';
import Dashboard from '../pages/Dashboard';
import DeviceBattery from '../pages/Devices/DeviceBattery';
import DeviceStorage from '../pages/Devices/DeviceStorage';
import ManageDevices from '../pages/Devices/ManageDevices';
import EditAccessProfile from '../pages/DeviceUsers/AccessProfile/EditAccessProfile';
import ManageAccessProfiles from '../pages/DeviceUsers/AccessProfile/ManageAccessProfiles';
import EditDeviceUsers from '../pages/DeviceUsers/EditDeviceUsers';
import ManageUsers from '../pages/DeviceUsers/ManageDeviceUsers';
import EditGeofence from '../pages/Geofences/EditGeofence';
import ManageGeofences from '../pages/Geofences/ManageGeofences';
import Geolocation from '../pages/Geolocation';
import EditGroup from '../pages/Groups/EditGroup';
import ManageGroups from '../pages/Groups/ManageGroups';
import FAQ from '../pages/Help/FAQ';
import Support from '../pages/Help/Support';
import Login from '../pages/Login';
import Unblock from '../pages/Login/Unblock';
import MessageDetails from '../pages/Messages/MessageDetails';
import MessagesEdit from '../pages/Messages/MessagesEdit';
import MessagesList from '../pages/Messages/MessagesList';
import PasswordRecovery from '../pages/PasswordRecovery';
import DeviceLocation from '../pages/Reports/DeviceLocation';
import LocationHistory from '../pages/Reports/LocationHistory';
import ReportDate from '../pages/Sites/ReportDate';
import ReportGps from '../pages/Sites/ReportGps';
import ReportUrl from '../pages/Sites/ReportUrl';
import EditSubGroup from '../pages/Subgroups/EditSubgroup';
import ManageSubgroups from '../pages/Subgroups/ManageSubgroups';

interface CustomRouteProps extends RouteProps {
  isPrivate?: boolean;
}

const CustomRoute = ({
  isPrivate,
  path,
  component,
  ...rest
}: CustomRouteProps) => (
  <Route
    {...rest}
    path={path}
    component={() => {
      const authStore = useAppSelector((state) => state.auth);
      const { pages: privilegePages } = usePrivilege();

      const pathArr: string[] = Array.isArray(path) ? path : [path];
      const { path: pathRoute } = useRouteMatch(pathArr);

      if (isPrivate && (!authStore.isLogged || !privilegePages[pathRoute])) {
        return <Redirect to={routes.login} />;
      }

      return component.apply({});
    }}
  />
);

export default function Routes() {
  return (
    <Switch>
      <CustomRoute exact path={routes.login} component={Login} />
      <CustomRoute
        exact
        path={routes.passwordRecovery}
        component={PasswordRecovery}
      />
      <CustomRoute exact path={routes.unblock} component={Unblock} />
      <Layout>
        <CustomRoute
          isPrivate
          exact
          path={routes.company.info}
          component={CompanyInfo}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.company.consumption}
          component={CompanyConsumption}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.company.license}
          component={CompanyLicense}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.groups.manage}
          component={ManageGroups}
        />
        <CustomRoute
          isPrivate
          exact
          path={[routes.groups.edit, routes.groups.register]}
          component={EditGroup}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.subgroups.manage}
          component={ManageSubgroups}
        />
        <CustomRoute
          isPrivate
          exact
          path={[routes.subgroups.edit, routes.subgroups.register]}
          component={EditSubGroup}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.adminUsers.manage}
          component={ManageAdminUsers}
        />
        <CustomRoute
          isPrivate
          exact
          path={[routes.adminUsers.edit, routes.adminUsers.register]}
          component={EditAdminUser}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.deviceUsers.manage}
          component={ManageUsers}
        />
        <CustomRoute
          isPrivate
          path={[routes.deviceUsers.edit]}
          component={EditDeviceUsers}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.application.manage}
          component={ManageApplications}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.application.applicationDeviceUsers}
          component={ApplicationDeviceUsers}
        />
        <CustomRoute
          isPrivate
          exact
          path={[
            routes.application.consumptionHistory,
            routes.application.consumptionHistoryByDeviceUser,
          ]}
          component={ConsumptionHistory}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.messages.list}
          component={MessagesList}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.messages.details}
          component={MessageDetails}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.messages.register}
          component={MessagesEdit}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.documents.list}
          component={MessagesList}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.documents.register}
          component={MessagesEdit}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.documents.details}
          component={MessageDetails}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.device.manage}
          component={ManageDevices}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.device.battery}
          component={DeviceBattery}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.device.storage}
          component={DeviceStorage}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.informes.locationHistory}
          component={LocationHistory}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.informes.deviceLocation}
          component={DeviceLocation}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.geolocation}
          component={Geolocation}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.support}
          component={Support}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.sites.reportUrl}
          component={ReportUrl}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.sites.reportDate}
          component={ReportDate}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.informes.reportGps}
          component={ReportGps}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.profileConsumption.manage}
          component={ConsumptionProfile}
        />
        <CustomRoute
          isPrivate
          exact
          path={[
            routes.profileConsumption.groups.register,
            routes.profileConsumption.groups.edit,
          ]}
          component={EditGroupConsumptionRole}
        />
        <CustomRoute
          isPrivate
          exact
          path={[
            routes.profileConsumption.subgroups.register,
            routes.profileConsumption.subgroups.edit,
          ]}
          component={EditSubgroupConsumptionRole}
        />
        <CustomRoute
          isPrivate
          exact
          path={[
            routes.profileConsumption.users.register,
            routes.profileConsumption.users.edit,
          ]}
          component={EditUserConsumptionRole}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.generalConfig.manage}
          component={GeneralConfig}
        />
        <CustomRoute
          isPrivate
          exact
          path={[routes.generalConfig.group.edit, routes.generalConfig.group.register]}
          component={GeneralConfigGroupEdit}
        />
        <CustomRoute
          isPrivate
          exact
          path={[routes.generalConfig.subgroup.edit, routes.generalConfig.subgroup.register]}
          component={GeneralConfigSubgroupEdit}
        />
        <CustomRoute
          isPrivate
          exact
          path={[routes.generalConfig.users.edit, routes.generalConfig.users.register]}
          component={GeneralConfigUserEdit}
        />

        <CustomRoute isPrivate exact path={routes.audit} component={Audit} />

        <CustomRoute
          isPrivate
          exact
          path={routes.profileAccess.manage}
          component={ManageAccessProfiles}
        />
        <CustomRoute
          isPrivate
          exact
          path={[routes.profileAccess.edit, routes.profileAccess.register]}
          component={EditAccessProfile}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.geofence.manage}
          component={ManageGeofences}
        />
        <CustomRoute
          isPrivate
          exact
          path={[routes.geofence.edit, routes.geofence.register]}
          component={EditGeofence}
        />

        <CustomRoute isPrivate exact path={routes.faq} component={FAQ} />

        <CustomRoute
          isPrivate
          exact
          path={routes.kioskMode}
          component={KioskMode}
        />

        <CustomRoute isPrivate exact path={routes.dashboard} component={Dashboard} />

        <CustomRoute
          isPrivate
          exact
          path={routes.applicationControl.manage}
          component={BlockApplication}
        />
        <CustomRoute
          isPrivate
          exact
          path={[
            routes.applicationControl.general.edit,
            routes.applicationControl.general.register,
          ]}
          component={BlockApplicationGeneralEdit}
        />
        <CustomRoute
          isPrivate
          exact
          path={[
            routes.applicationControl.group.edit,
            routes.applicationControl.group.register,
          ]}
          component={BlockApplicationGroupEdit}
        />
        <CustomRoute
          isPrivate
          exact
          path={[
            routes.applicationControl.subgroup.edit,
            routes.applicationControl.subgroup.register,
          ]}
          component={BlockApplicationSubgroupEdit}
        />
        <CustomRoute
          isPrivate
          exact
          path={[
            routes.applicationControl.user.edit,
            routes.applicationControl.user.register,
          ]}
          component={BlockApplicationUserEdit}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.applicationConfig.manage}
          component={ApplicationManage}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.applicationConfig.register}
          component={ApplicationRegister}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.applicationConfig.history}
          component={ApplicationHistory}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.applicationConfig.historyStatus}
          component={ApplicationSendStatus}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.siteControl.category}
          component={BlockSiteCategory}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.siteControl.manage}
          component={BlockSiteUrl}
        />
        <CustomRoute
          isPrivate
          exact
          path={[routes.siteControl.general.edit, routes.siteControl.general.register]}
          component={BlockSiteUrlGeneralEdit}
        />
        <CustomRoute
          isPrivate
          exact
          path={[routes.siteControl.group.edit, routes.siteControl.group.register]}
          component={BlockSiteUrlGroupEdit}
        />
        <CustomRoute
          isPrivate
          exact
          path={[routes.siteControl.subgroup.edit, routes.siteControl.subgroup.register]}
          component={BlockSiteUrlSubgroupEdit}
        />
        <CustomRoute
          isPrivate
          exact
          path={[routes.siteControl.user.edit, routes.siteControl.user.register]}
          component={BlockSiteUrlUserEdit}
        />

        <CustomRoute
          isPrivate
          exact
          path={routes.qrcode.generate}
          component={QRCode}
        />
        <CustomRoute
          isPrivate
          exact
          path={routes.qrcode.show}
          component={QRCodeDeviceOwner}
        />

        <CustomRoute isPrivate path={routes.home} component={() => <></>} />
      </Layout>
    </Switch>
  );
}
