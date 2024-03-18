import { PrivilegeEnum } from '../helper';
import routes from '../routes';
import { useAppSelector } from './useRedux';

export const usePrivilege = () => {
  const { user } = useAppSelector((state) => state.auth);
  const privilege = user?.privilegeId;

  const companyOnly = [PrivilegeEnum.COMPANY];
  const companyGroup = [PrivilegeEnum.COMPANY, PrivilegeEnum.GROUP];
  const companyGroupSubgroup = [
    PrivilegeEnum.COMPANY,
    PrivilegeEnum.GROUP,
    PrivilegeEnum.SUBGROUP,
  ];

  const menuItems = {
    dashboard: companyGroupSubgroup.includes(privilege),
    company: companyGroupSubgroup.includes(privilege),
    groups: companyGroupSubgroup.includes(privilege),
    users: companyGroupSubgroup.includes(privilege),
    devices: companyGroupSubgroup.includes(privilege),
    applications: companyGroupSubgroup.includes(privilege),
    browser: companyGroupSubgroup.includes(privilege),
    messages: companyGroupSubgroup.includes(privilege),
    deviceLocation: companyGroupSubgroup.includes(privilege),
    sharedDocs: companyGroupSubgroup.includes(privilege),
    geolocation: companyGroupSubgroup.includes(privilege),
    documents: companyGroupSubgroup.includes(privilege),
    androidActions: [].includes(privilege),
    config: companyGroupSubgroup.includes(privilege),
    help: companyGroupSubgroup.includes(privilege),
  };

  const menuSubitems = {
    company: companyOnly.includes(privilege),
    groups: companyGroup.includes(privilege),
    subgroups: companyGroupSubgroup.includes(privilege),
    deviceUsers: companyGroupSubgroup.includes(privilege),
    adminUsers: companyGroupSubgroup.includes(privilege),
    browser: companyGroupSubgroup.includes(privilege),
    documents: companyGroupSubgroup.includes(privilege),
    androidActions: [].includes(privilege),
    androidActionsBlock: [].includes(privilege),
    androidActionsConfig: [].includes(privilege),
    geofences: [].includes(privilege),
    config: companyGroupSubgroup.includes(privilege),
    help: companyGroupSubgroup.includes(privilege),
  };

  const pages: { [key in keyof typeof routes]?: boolean } = {
    [routes.home]: companyGroupSubgroup.includes(privilege),
    [routes.dashboard]: companyGroupSubgroup.includes(privilege),
    [routes.company.info]: companyOnly.includes(privilege),
    [routes.company.consumption]: companyOnly.includes(privilege),
    [routes.company.license]: companyOnly.includes(privilege),
    [routes.groups.manage]: companyGroup.includes(privilege),
    [routes.groups.register]: companyGroup.includes(privilege),
    [routes.groups.edit]: companyGroup.includes(privilege),
    [routes.subgroups.manage]: companyGroupSubgroup.includes(privilege),
    [routes.subgroups.register]: companyGroupSubgroup.includes(privilege),
    [routes.subgroups.edit]: companyGroupSubgroup.includes(privilege),
    [routes.adminUsers.manage]: companyGroupSubgroup.includes(privilege),
    [routes.adminUsers.register]: companyGroupSubgroup.includes(privilege),
    [routes.adminUsers.edit]: companyGroupSubgroup.includes(privilege),
    [routes.deviceUsers.manage]: companyGroupSubgroup.includes(privilege),
    [routes.deviceUsers.edit]: companyGroupSubgroup.includes(privilege),
    [routes.sites.reportDate]: companyGroupSubgroup.includes(privilege),
    [routes.sites.reportUrl]: companyGroupSubgroup.includes(privilege),
    [routes.informes.reportGps]: companyGroupSubgroup.includes(privilege),
    [routes.application.manage]: companyGroupSubgroup.includes(privilege),
    [routes.application.applicationDeviceUsers]:
      companyGroupSubgroup.includes(privilege),
    [routes.application.consumptionHistory]:
      companyGroupSubgroup.includes(privilege),
    [routes.application.consumptionHistoryByDeviceUser]:
      companyGroupSubgroup.includes(privilege),
    [routes.messages.list]: companyGroupSubgroup.includes(privilege),
    [routes.messages.details]: companyGroupSubgroup.includes(privilege),
    [routes.messages.register]: companyGroupSubgroup.includes(privilege),
    [routes.informes.deviceLocation]: companyGroupSubgroup.includes(privilege),
    [routes.documents.list]: companyGroupSubgroup.includes(privilege),
    [routes.documents.details]: companyGroupSubgroup.includes(privilege),
    [routes.documents.register]: companyGroupSubgroup.includes(privilege),
    [routes.device.manage]: companyGroupSubgroup.includes(privilege),
    [routes.device.battery]: companyGroupSubgroup.includes(privilege),
    [routes.device.storage]: companyGroupSubgroup.includes(privilege),
    [routes.informes.locationHistory]: companyGroupSubgroup.includes(privilege),
    [routes.geolocation]: companyGroupSubgroup.includes(privilege),
    [routes.profileConsumption.manage]:
      companyGroupSubgroup.includes(privilege),
    [routes.profileConsumption.groups.register]:
      companyGroup.includes(privilege),
    [routes.profileConsumption.groups.edit]: companyGroup.includes(privilege),
    [routes.profileConsumption.subgroups.register]:
      companyGroupSubgroup.includes(privilege),
    [routes.profileConsumption.subgroups.edit]:
      companyGroupSubgroup.includes(privilege),
    [routes.profileConsumption.users.register]:
      companyGroupSubgroup.includes(privilege),
    [routes.profileConsumption.users.edit]:
      companyGroupSubgroup.includes(privilege),
    [routes.generalConfig.manage]: [].includes(privilege),
    [routes.generalConfig.group.edit]: [].includes(privilege),
    [routes.generalConfig.group.register]: [].includes(privilege),
    [routes.generalConfig.subgroup.edit]: [].includes(privilege),
    [routes.generalConfig.subgroup.register]: [].includes(privilege),
    [routes.generalConfig.users.register]: [].includes(privilege),
    [routes.generalConfig.users.edit]: [].includes(privilege),
    [routes.profileAccess.manage]: [].includes(privilege),
    [routes.profileAccess.edit]: [].includes(privilege),
    [routes.profileAccess.register]: [].includes(privilege),
    [routes.geofence.manage]: [].includes(privilege),
    [routes.geofence.register]: [].includes(privilege),
    [routes.geofence.edit]: [].includes(privilege),
    [routes.applicationControl.manage]:
      companyGroupSubgroup.includes(privilege),
    [routes.applicationControl.general.register]:
      companyOnly.includes(privilege),
    [routes.applicationControl.general.edit]: companyOnly.includes(privilege),
    [routes.applicationControl.group.register]:
      companyGroup.includes(privilege),
    [routes.applicationControl.group.edit]: companyGroup.includes(privilege),
    [routes.applicationControl.subgroup.register]:
      companyGroupSubgroup.includes(privilege),
    [routes.applicationControl.subgroup.edit]:
      companyGroupSubgroup.includes(privilege),
    [routes.applicationControl.user.register]:
      companyGroupSubgroup.includes(privilege),
    [routes.applicationControl.user.edit]:
      companyGroupSubgroup.includes(privilege),
    [routes.applicationConfig.manage]: [].includes(privilege),
    [routes.applicationConfig.register]: [].includes(privilege),
    [routes.applicationConfig.history]: [].includes(privilege),
    [routes.applicationConfig.historyStatus]: [].includes(privilege),
    [routes.support]: [].includes(privilege),
    [routes.audit]: [].includes(privilege),
    [routes.faq]: [].includes(privilege),
    [routes.kioskMode]: [].includes(privilege),
    [routes.siteControl.category]: [].includes(privilege),
    [routes.siteControl.manage]: [].includes(privilege),
    [routes.siteControl.general.register]: [].includes(privilege),
    [routes.siteControl.general.edit]: [].includes(privilege),
    [routes.siteControl.group.register]: [].includes(privilege),
    [routes.siteControl.group.edit]: [].includes(privilege),
    [routes.siteControl.subgroup.register]: [].includes(privilege),
    [routes.siteControl.subgroup.edit]: [].includes(privilege),
    [routes.siteControl.user.register]: [].includes(privilege),
    [routes.siteControl.user.edit]: [].includes(privilege),
    [routes.qrcode.generate]: companyGroupSubgroup.includes(privilege),
    [routes.qrcode.show]: companyGroupSubgroup.includes(privilege),
  };

  return {
    menuItems,
    menuSubitems,
    pages,
    accessFilterCompany: [PrivilegeEnum.COMPANY].includes(privilege),
    accessFilterGroup: [PrivilegeEnum.COMPANY, PrivilegeEnum.GROUP].includes(
      privilege
    ),
    accessFilterSubgroup: [
      PrivilegeEnum.COMPANY,
      PrivilegeEnum.GROUP,
      PrivilegeEnum.SUBGROUP,
    ].includes(privilege),
  };
};
