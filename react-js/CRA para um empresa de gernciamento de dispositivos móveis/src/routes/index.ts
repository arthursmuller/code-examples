const routes = {
  login: '/login',
  passwordRecovery: '/password-recovery',
  unblock: '/unblock',
  home: '/',
  dashboard: '/dashboard',
  company: {
    info: '/company-info',
    consumption: '/company-consumption',
    license: '/company-license',
  },
  groups: {
    manage: '/manage-groups',
    register: '/edit-group',
    edit: '/edit-group/:id',
  },
  subgroups: {
    manage: '/manage-subgroups',
    register: '/edit-subgroup',
    edit: '/edit-subgroup/:id',
  },
  adminUsers: {
    manage: '/manage-admin-users',
    register: '/edit-admin-users',
    edit: '/edit-admin-users/:id',
  },
  deviceUsers: {
    manage: '/manage-device-users',
    edit: '/edit-device-user/:id',
  },
  sites: {
    reportUrl: '/report-url',
    reportDate: '/report-date',
  },
  informes: {
    reportGps: '/report-gps',
    deviceLocation: '/device-location',
    locationHistory: '/location-history',
  },
  application: {
    manage: '/manage-applications',
    applicationDeviceUsers: '/application-device-users/:applicationName',
    consumptionHistory: '/consumption-history/:applicationName',
    consumptionHistoryByDeviceUser:
      '/consumption-history/:applicationName/:deviceUserId',
  },
  messages: {
    list: '/messages-list',
    register: '/messages-register',
    details: '/message-details/:id',
  },
  documents: {
    list: '/documents-list',
    register: '/document-register',
    details: '/document-details/:id',
  },
  device: {
    manage: '/manage-device',
    battery: '/device-battery/:id',
    storage: '/device-storage/:id',
  },
  geolocation: '/geolocation',
  profileConsumption: {
    manage: '/consumption-profile',
    groups: {
      register: '/consumption-profile/groups/register',
      edit: '/consumption-profile/groups/edit/:id',
    },
    subgroups: {
      register: '/consumption-profile/subgroups/register',
      edit: '/consumption-profile/subgroups/edit/:id',
    },
    users: {
      register: '/consumption-profile/users/register',
      edit: '/consumption-profile/user/edit/:id',
    },
  },
  generalConfig: {
    manage: '/general-config',
    group: {
      edit: '/general-config-edit/:id',
      register: '/general-config-register',
    },
    subgroup: {
      edit: '/general-config-subgroup-edit',
      register: '/general-config-subgroup-register',
    },
    users: {
      register: '/general-config-users-register',
      edit: '/general-config-users-edit',
    },
  },
  profileAccess: {
    manage: '/manage-access-profiles',
    edit: '/edit-access-profile',
    register: '/register-access-profile',
  },
  geofence: {
    manage: '/manage-geofences',
    register: '/register-geofence',
    edit: '/edit-geofence/:id',
  },
  applicationControl: {
    manage: '/android/block-app',
    general: {
      register: '/android/block-app/register-general',
      edit: '/android/block-app/edit-general/:id',
    },
    group: {
      register: '/android/block-app/register-group',
      edit: '/android/block-app/edit-group/:id',
    },
    subgroup: {
      register: '/android/block-app/register-subgroup',
      edit: '/android/block-app/edit-subgroup/:id',
    },
    user: {
      register: '/android/block-app/register-user',
      edit: '/android/block-app/edit-user/:id',
    },
  },
  applicationConfig: {
    manage: '/android/manage-application',
    register: '/android/register-application',
    history: '/android/history-application/:id',
    historyStatus: '/android/history-application/:id/status/:history',
  },
  support: '/support',
  audit: '/audit',
  faq: '/faq',
  kioskMode: '/kiosk-mode',
  siteControl: {
    category: '/block-site-category',
    manage: '/block-site-url',
    general: {
      register: '/block-site-url-general-register',
      edit: '/block-site-url-general-edit',
    },
    group: {
      register: '/block-site-url-group-register',
      edit: '/block-site-url-group-edit',
    },
    subgroup: {
      register: '/block-site-url-subgroup-register',
      edit: '/block-site-url-subgroup-edit',
    },
    user: {
      register: '/block-site-url-user-register',
      edit: '/block-site-url-user-edit',
    },
  },
  qrcode: {
    generate: '/qrcode',
    show: '/qrcode-device-owner',
  },
};

export default routes;
