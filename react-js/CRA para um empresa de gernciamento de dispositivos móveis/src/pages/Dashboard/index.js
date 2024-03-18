import { Flex, Box } from '@chakra-ui/react';
import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import BadgeIcon from '../../components/Icons/Badge';
import UploadIcon from '../../components/Icons/Upload';
import UserIcon from '../../components/Icons/User';
import { listDeviceUsers } from '../../store/deviceUser';
import { listGroups } from '../../store/group';
import { listSubgroups } from '../../store/subgroup';
import BlockedWebsites from './compoenents/BlockedWebsites';
import CardHeading from './compoenents/CardHeading';
import DashboardCard from './compoenents/DashboardCard';
import DeviceInventoryChart from './compoenents/DeviceInventoryChart';
import DevicesCard from './compoenents/DevicesCard';
import DevicesLocationMap from './compoenents/DevicesLocationMap';
import Filters from './compoenents/Filters';
import GuidesForDownloadCard from './compoenents/GuidesForDownloadCard';
import Header from './compoenents/Header';
import HelpCard from './compoenents/HelpCard';
import HorizontalBarChart from './compoenents/HorizontalBarChart';
import LastActivitiesTable from './compoenents/LastActivitiesTable';
import MonthlyClusterBarChart from './compoenents/MonthlyClusterBarChart';
import MonthlyClusterBarChartUsers from './compoenents/MonthlyClusterBarChartUsers';
import PieChart from './compoenents/PieChart';
import VideoTutorialsCard from './compoenents/VideoTutorialsCard';

function Dashboard() {
  const dispatch = useDispatch();
  const {
    blockedWebsites,
    lastActivities,
    dataConsumptionPieChart,
    smsConsumptionPieChart,
    applicationConsumption,
    userConsumption,
    applicationConsumptionTime,
    visitedWebsites,
    inventoryPieChart,
    consumption,
    users,
    mapMarkers,
    users_total_heading,
    uninstalled_applications,
    uninstall_attempts,
    not_activated_licenses,
  } = useSelector((state) => state.dashboard);
  const { groups } = useSelector((state) => state.group);
  const { subgroups } = useSelector((state) => state.subgroup);
  const { deviceUsers: usersList } = useSelector((state) => state.deviceUser);
  const iconBoxSize = 12;
  const iconColor = '#d7d7dc';

  useEffect(() => {
    dispatch(listGroups());
    dispatch(listSubgroups());
    dispatch(listDeviceUsers());
  }, []);

  return (
    <Box w="90%">
      <Header />
      <Filters groups={groups} subgroups={subgroups} users={usersList} />
      <Flex justify="space-between">
        <CardHeading
          title="dashboard.users_total_heading"
          icon={<UserIcon boxSize={iconBoxSize} color={iconColor} />}
          qty={users_total_heading}
        />
        <CardHeading
          title="dashboard.uninstalled_applications"
          icon={<UploadIcon boxSize={iconBoxSize} color={iconColor} />}
          qty={uninstalled_applications}
          qtyColor="#de6163"
        />
        <CardHeading
          title="dashbaord.uninstall_attempts"
          icon={<UploadIcon boxSize={iconBoxSize} color={iconColor} />}
          qty={uninstall_attempts}
        />
        <CardHeading
          title="dashboard.not_activated_licenses"
          icon={<BadgeIcon boxSize={iconBoxSize} color={iconColor} />}
          qty={not_activated_licenses}
          qtyColor="#de6163"
          last
        />
      </Flex>
      <Flex justify="space-between" mt="40px">
        <DevicesCard />
        <DashboardCard title="Consumo de dados">
          <PieChart data={dataConsumptionPieChart} />
        </DashboardCard>
        <DashboardCard title="Consumo de SMS" last>
          <PieChart data={smsConsumptionPieChart} />
        </DashboardCard>
      </Flex>
      <Flex justify="space-between" mt="40px">
        <DashboardCard title="Consumo de datos por aplicaciones">
          {/* <HorizontalBarChart data={applicationConsumption} /> */}
        </DashboardCard>
        <DashboardCard title="Consumo de datos por usuario" last>
          {/* <HorizontalBarChart data={userConsumption} /> */}
        </DashboardCard>
      </Flex>
      <Flex justify="space-between" mt="40px">
        <DashboardCard title="Tiempo de uso por aplicaciones">
          {/* <HorizontalBarChart data={applicationConsumptionTime} /> */}
        </DashboardCard>
        <DashboardCard title="Sitios bloqueados más visitados" last>
          <BlockedWebsites data={blockedWebsites} />
        </DashboardCard>
      </Flex>
      <Flex justify="space-between" mt="40px">
        <DashboardCard title=" Sitios más visitados">
          {/* <HorizontalBarChart data={visitedWebsites} /> */}
        </DashboardCard>
        <DashboardCard title="Inventário de dispositivos" last>
          {/* <DeviceInventoryChart data={inventoryPieChart} /> */}
        </DashboardCard>
      </Flex>
      <Flex justify="space-between" mt="40px">
        <DashboardCard title="Consumo">
          {/* <MonthlyClusterBarChart data={consumption} /> */}
        </DashboardCard>
        <DashboardCard title="Total de usuarios" last>
          {/* <MonthlyClusterBarChartUsers data={users} /> */}
        </DashboardCard>
      </Flex>
      <Flex>
        <DevicesLocationMap data={mapMarkers} />
      </Flex>
      <Flex>
        <DashboardCard title="Ultimas atividades realizadas">
          <LastActivitiesTable data={lastActivities} />
        </DashboardCard>
        <Flex flexDirection="column">
          <GuidesForDownloadCard />
          <VideoTutorialsCard />
        </Flex>
      </Flex>
      <Flex>
        <HelpCard />
      </Flex>
    </Box>
  );
}

export default Dashboard;
