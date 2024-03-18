import { usePrivilege } from '../../hooks/usePrivilege';
import routes from '../../routes';
import AndroidIcon from '../Icons/Android';
import AppIcon from '../Icons/App';
import BrowserIcon from '../Icons/Browser';
import CompanyIcon from '../Icons/Company';
import ConfigIcon from '../Icons/Config';
import DeviceIcon from '../Icons/Device';
import DocumentIcon from '../Icons/Documents';
import GpsIcon from '../Icons/GPS';
import GroupsIcon from '../Icons/Groups';
import HelpIcon from '../Icons/Help';
import HomeIcon from '../Icons/Home';
import MessageIcon from '../Icons/Message';
import ShareIcon from '../Icons/Share';
import UserIcon from '../Icons/User';

interface MenuLink {
  link: string;
  title: string;
}

interface MenuSubitem {
  id: string;
  title: string;
  titleStyle?: string;
  drilldown: MenuLink[];
}

export interface MenuItem {
  id: string;
  title: string;
  link?: string;
  icon: React.ReactNode;
  drilldown?: MenuSubitem[];
}

function menuItems() {
  const privilegio = usePrivilege();
  //
  // company drill down
  const mountCompanyDrilldownMenu = () => {
    const menuItems: MenuSubitem[] = [];

    menuItems.push({
      id: 'company',
      title: 'Empresas',
      drilldown: [
        {
          title: 'Información de la empresa',
          link: routes.company.info,
        },
        {
          title: 'Consumos',
          link: routes.company.consumption,
        },
        {
          title: 'Licencias contratadas',
          link: routes.company.license,
        },
      ],
    });

    return menuItems;
  };

  //
  // groups drill down
  const mountGroupsDrilldownMenu = () => {
    const menuItems: MenuSubitem[] = [];

    menuItems.push({
      id: 'groups',
      title: 'Grupos',
      drilldown: [
        {
          title: 'Cadastrar novo grupo',
          link: routes.groups.register,
        },
        {
          title: 'Gerenciar grupos',
          link: routes.groups.manage,
        },
      ],
    });

    menuItems.push({
      id: 'subgroups',
      title: 'Subgrupos',
      drilldown: [
        {
          title: 'Cadastrar novo subgrupo',
          link: routes.subgroups.register,
        },
        {
          title: 'Gerenciar subgrupos',
          link: routes.subgroups.manage,
        },
      ],
    });

    return menuItems;
  };

  const mountSitesDrilldownMenu = () => {
    const menuItems: MenuSubitem[] = [];

    menuItems.push({
      id: 'browser',
      title: 'Sites',
      drilldown: [
        {
          title: 'Informe por Fecha',
          link: routes.sites.reportDate,
        },
        {
          title: 'Informe por URL / Palabra clave',
          link: routes.sites.reportUrl,
        },
      ],
    });

    return menuItems;
  };

  const mountUsersDrilldownMenu = () => {
    const menuItems: MenuSubitem[] = [];

    menuItems.push({
      id: 'deviceUsers',
      title: 'Usuários',
      drilldown: [
        {
          title: 'Gerenciar usuários de dispositivo',
          link: routes.deviceUsers.manage,
        },
        {
          title: 'Perfis de scesso',
          link: routes.profileAccess.manage,
        },
      ],
    });

    menuItems.push({
      id: 'adminUsers',
      title: 'Administradores',
      titleStyle: 'sub',
      drilldown: [
        {
          title: 'Cadastrar novo usuário administrador',
          link: routes.adminUsers.register,
        },
        {
          title: 'Gerenciar usuários administradores',
          link: routes.adminUsers.manage,
        },
      ],
    });

    return menuItems;
  };

  //
  // informes
  const mountInformesDrilldownMenu = () => {
    const menuItems: MenuSubitem[] = [];

    menuItems.push({
      id: 'documents',
      title: 'Informes',
      drilldown: [
        {
          title: 'GPS',
          link: routes.informes.reportGps,
        },
        {
          title: 'Ubicación de dispositivos',
          link: routes.informes.deviceLocation,
        },
        {
          title: 'Histórico de Ubicación',
          link: routes.informes.locationHistory,
        },
      ],
    });

    return menuItems;
  };

  //
  // adroid actions
  const mountAndroidActionsDrilldownMenu = () => {
    const menuItems: MenuSubitem[] = [];

    menuItems.push({
      id: 'androidActions',
      title: 'Acciones Android',
      drilldown: [],
    });

    menuItems.push({
      id: 'androidActionsBlock',
      title: 'Bloquear / Desbloquear',
      titleStyle: 'sub',
      drilldown: [
        {
          title: 'Aplicaciones',
          link: routes.applicationControl.manage,
        },
        {
          title: 'Sitios web por categoria',
          link: routes.siteControl.category,
        },
        {
          title: 'Sitios web por URL',
          link: routes.siteControl.manage,
        },
      ],
    });

    menuItems.push({
      id: 'androidActionsConfig',
      title: 'Configurações',
      titleStyle: 'sub',
      drilldown: [
        {
          title: 'Habilitar modo Kiosko',
          link: routes.kioskMode,
        },
        {
          title: 'Instalar / Actualizar aplicación',
          link: routes.applicationConfig.manage,
        },
      ],
    });

    menuItems.push({
      id: 'geofences',
      title: 'Geofences',
      titleStyle: 'sub',
      drilldown: [
        {
          title: 'Cadastrar nova geofence',
          link: routes.geofence.register,
        },
        {
          title: 'Gerenciar geofences',
          link: routes.geofence.manage,
        },
      ],
    });

    return menuItems;
  };

  //
  // config
  const mountConfigDrilldownMenu = () => {
    const menuItems: MenuSubitem[] = [];

    menuItems.push({
      id: 'config',
      title: 'Configuraciones',
      drilldown: [
        {
          title: 'Configuraciones Generales',
          link: routes.generalConfig.manage,
        },
        {
          title: 'Perfil de consumo ',
          link: routes.profileConsumption.manage,
        },
        {
          title: 'Código QR para Device Owner',
          link: routes.qrcode.generate,
        },
        {
          title: 'Auditoria',
          link: routes.audit,
        },
      ],
    });

    return menuItems;
  };

  //
  // help
  const mountHelpDrilldownMenu = () => {
    const menuItems: MenuSubitem[] = [];

    menuItems.push({
      id: 'help',
      title: 'Ayuda',
      drilldown: [
        {
          title: 'Preguntas frecuentes',
          link: routes.faq,
        },
        {
          title: 'Documentos y materiales de apoyo',
          link: routes.support,
        },
        {
          title: 'Solicitud de Soporte',
          link: routes.home,
        },
      ],
    });

    return menuItems;
  };

  //
  // ------------------------------
  // Start mounting root menus
  // ------------------------------
  const menuList: MenuItem[] = [];
  const desktopMenuBoxSize = 6;
  const desktopMenuColor = 'white';

  //
  // dashboard menu
  menuList.push({
    id: 'dashboard',
    title: 'Dashboard',
    link: routes.dashboard,
    icon: <HomeIcon boxSize={desktopMenuBoxSize} color={desktopMenuColor} />,
  });

  //
  // company info menu
  menuList.push({
    id: 'company',
    title: 'Empresas',
    icon: <CompanyIcon boxSize={desktopMenuBoxSize} color={desktopMenuColor} />,
    drilldown: [].concat(mountCompanyDrilldownMenu()),
  });

  //
  // manage groups menu
  menuList.push({
    id: 'groups',
    title: 'Grupos e subgrupos',
    icon: <GroupsIcon boxSize={desktopMenuBoxSize} color={desktopMenuColor} />,
    drilldown: [].concat(mountGroupsDrilldownMenu()),
  });

  //
  // manage users menu
  menuList.push({
    id: 'users',
    title: 'Usuários',
    icon: <UserIcon boxSize={desktopMenuBoxSize} color={desktopMenuColor} />,
    drilldown: [].concat(mountUsersDrilldownMenu()),
  });

  //
  // device menu
  menuList.push({
    id: 'devices',
    title: 'Dispositivos',
    link: routes.device.manage,
    icon: <DeviceIcon boxSize={desktopMenuBoxSize} color={desktopMenuColor} />,
  });

  //
  // manage applications menu
  menuList.push({
    id: 'applications',
    title: 'Aplicações',
    link: routes.application.manage,
    icon: <AppIcon boxSize={desktopMenuBoxSize} color={desktopMenuColor} />,
  });

  //
  // browser menu
  menuList.push({
    id: 'browser',
    title: 'Sitios',
    icon: <BrowserIcon boxSize={desktopMenuBoxSize} color={desktopMenuColor} />,
    drilldown: [].concat(mountSitesDrilldownMenu()),
  });

  //
  // messages menu
  menuList.push({
    id: 'messages',
    title: 'Mensagens',
    link: routes.messages.list,
    icon: <MessageIcon boxSize={desktopMenuBoxSize} color={desktopMenuColor} />,
  });

  //
  // shared documents menu
  menuList.push({
    id: 'sharedDocs',
    title: 'Compartilhar documento',
    link: routes.documents.list,
    icon: <ShareIcon boxSize={desktopMenuBoxSize} color={desktopMenuColor} />,
  });

  //
  // gps menu
  menuList.push({
    id: 'geolocation',
    title: 'Geolocalização',
    link: routes.geolocation,
    icon: <GpsIcon boxSize={desktopMenuBoxSize} color={desktopMenuColor} />,
  });

  //
  // document menu
  menuList.push({
    id: 'documents',
    title: 'Informes',
    icon: (
      <DocumentIcon boxSize={desktopMenuBoxSize} color={desktopMenuColor} />
    ),
    drilldown: [].concat(mountInformesDrilldownMenu()),
  });

  //
  // android menu
  menuList.push({
    id: 'androidActions',
    title: 'Acciones Android',
    icon: <AndroidIcon boxSize={desktopMenuBoxSize} color={desktopMenuColor} />,
    drilldown: [].concat(mountAndroidActionsDrilldownMenu()),
  });

  //
  // config menu
  menuList.push({
    id: 'config',
    title: 'Configurações',
    icon: <ConfigIcon boxSize={desktopMenuBoxSize} color={desktopMenuColor} />,
    drilldown: [].concat(mountConfigDrilldownMenu()),
  });

  //
  // config menu
  menuList.push({
    id: 'help',
    title: 'Ajuda',
    icon: <HelpIcon boxSize={desktopMenuBoxSize} color={desktopMenuColor} />,
    drilldown: [].concat(mountHelpDrilldownMenu()),
  });

  const menuListAuthorized = menuList
    .filter((item) => !!privilegio.menuItems[item.id])
    .map((item) => ({
      ...item,
      drilldown: item.drilldown?.filter(
        (subItem) => !!privilegio.menuSubitems[subItem.id]
      ),
    }))
    .map((item) => ({
      ...item,
      drilldown: item.drilldown?.map((subItem) => ({
        ...subItem,
        drilldown: subItem.drilldown?.filter(
          (linkItem) => !!privilegio.pages[linkItem.link]
        ),
      })),
    }));

  return menuListAuthorized;
}

export default menuItems;
