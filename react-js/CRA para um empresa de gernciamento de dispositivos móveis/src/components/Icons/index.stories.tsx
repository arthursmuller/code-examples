import React from 'react';

import IconAlert from './Alert';
import IconAlertModal from './AlertModal';
import IconAndroid from './Android';
import IconApp from './App';
import IconBadge from './Badge';
import IconBell from './Bell';
import IconBrowser from './Browser';
import IconCheckmark from './Checkmark';
import IconCompany from './Company';
import IconConfig from './Config';
import IconCopy from './Copy';
import IconDate from './Date';
import IconDelete from './Delete';
import IconDevice from './Device';
import IconDocuments from './Documents';
import IconDownload from './Download';
import IconEdit from './Edit';
import IconExpand from './Expand';
import IconEye from './Eye';
import IconEyeDisabled from './EyeDisabled';
import IconGPS from './GPS';
import IconGraph from './Graph';
import IconGroups from './Groups';
import IconHelp from './Help';
import IconHome from './Home';
import IconLeftArrowCircle from './LeftArrowCircle';
import IconLock from './Lock';
import IconMapInitialPosition from './MapInitialPosition';
import IconMapLastPosition from './MapLastPosition';
import IconMapPresentPosition from './MapPresentPosition';
import IconMenu from './Menu';
import IconMessage from './Message';
import IconPerson from './Person';
import IconRefresh from './Refresh';
import IconSearch from './Search';
import IconShare from './Share';
import IconThreeDots from './ThreeDots';
import IconUpload from './Upload';
import IconUser from './User';

export default {
  title: 'Icons/Todos',
};

const Template = (Component: (props) => JSX.Element) => (args) => <Component {...args} />;

const designProps = {
  color: '#F0F',
  boxSize: 100,
};

export const Alert = Template(IconAlert).bind({});
Alert.args = { ...designProps };

export const AlertModal = Template(IconAlertModal).bind({});
AlertModal.args = { ...designProps };

export const Android = Template(IconAndroid).bind({});
Android.args = { ...designProps };

export const App = Template(IconApp).bind({});
App.args = { ...designProps };

export const Badge = Template(IconBadge).bind({});
Badge.args = { ...designProps };

export const Bell = Template(IconBell).bind({});
Bell.args = { ...designProps };

export const Browser = Template(IconBrowser).bind({});
Browser.args = { ...designProps };

export const Checkmark = Template(IconCheckmark).bind({});
Checkmark.args = { ...designProps };

export const Company = Template(IconCompany).bind({});
Company.args = { ...designProps };

export const Config = Template(IconConfig).bind({});
Config.args = { ...designProps };

export const Copy = Template(IconCopy).bind({});
Copy.args = { ...designProps };

export const Date = Template(IconDate).bind({});
Date.args = { ...designProps };

export const Delete = Template(IconDelete).bind({});
Delete.args = { ...designProps };

export const Device = Template(IconDevice).bind({});
Device.args = { ...designProps };

export const Documents = Template(IconDocuments).bind({});
Documents.args = { ...designProps };

export const Download = Template(IconDownload).bind({});
Download.args = { ...designProps };

export const Edit = Template(IconEdit).bind({});
Edit.args = { ...designProps };

export const Expand = Template(IconExpand).bind({});
Expand.args = { ...designProps };

export const Eye = Template(IconEye).bind({});
Eye.args = { ...designProps };

export const EyeDisabled = Template(IconEyeDisabled).bind({});
EyeDisabled.args = { ...designProps };

export const GPS = Template(IconGPS).bind({});
GPS.args = { ...designProps };

export const Graph = Template(IconGraph).bind({});
Graph.args = { ...designProps };

export const Groups = Template(IconGroups).bind({});
Groups.args = { ...designProps };

export const Help = Template(IconHelp).bind({});
Help.args = { ...designProps };

export const Home = Template(IconHome).bind({});
Home.args = { ...designProps };

export const LeftArrowCircle = Template(IconLeftArrowCircle).bind({});
LeftArrowCircle.args = { ...designProps };

export const Lock = Template(IconLock).bind({});
Lock.args = { ...designProps };

export const MapInitialPosition = Template(IconMapInitialPosition).bind({});
MapInitialPosition.args = { ...designProps };

export const MapLastPosition = Template(IconMapLastPosition).bind({});
MapLastPosition.args = { ...designProps };

export const MapPresentPosition = Template(IconMapPresentPosition).bind({});
MapPresentPosition.args = { ...designProps };

export const Menu = Template(IconMenu).bind({});
Menu.args = { ...designProps };

export const Message = Template(IconMessage).bind({});
Message.args = { ...designProps };

export const Person = Template(IconPerson).bind({});
Person.args = { ...designProps };

export const Refresh = Template(IconRefresh).bind({});
Refresh.args = { ...designProps };

export const Search = Template(IconSearch).bind({});
Search.args = { ...designProps };

export const Share = Template(IconShare).bind({});
Share.args = { ...designProps };

export const ThreeDots = Template(IconThreeDots).bind({});
ThreeDots.args = { ...designProps };

export const Upload = Template(IconUpload).bind({});
Upload.args = { ...designProps };

export const User = Template(IconUser).bind({});
User.args = { ...designProps };

