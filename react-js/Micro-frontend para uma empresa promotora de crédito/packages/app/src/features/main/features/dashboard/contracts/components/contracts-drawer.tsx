import { FC } from 'react';

import {
  Drawer,
  DrawerProps,
  formatCurrency,
  NoDataDisplay,
  Loader,
  BemErrorBoundary,
} from '@pcf/design-system';
import {
  ArrowLeftIcon,
  TabPropostasInativaIcon,
} from '@pcf/design-system-icons';
import { ContratoModel, getContratosQueryConfig, Resource } from '@pcf/core';

import { DashboardCardDrawer } from '../../components/dashboard-card-drawer';
import { DataDisplay } from '../../components/data-display';

export const ContractsDrawer: FC<DrawerProps> = (props) => {
  return (
    <Drawer
      {...props}
      content={({ onClose }) => (
        <>
          <Drawer.Title
            onClick={onClose}
            icon={ArrowLeftIcon}
            iconProps={{
              marginRight: '2px',
              width: '10.67px',
              height: '16px',
              fill: 'white',
            }}
            title="Meus Contratos"
            color="secondary.mid-dark"
          />
          <Drawer.Body>
            <BemErrorBoundary>
              <Resource<ContratoModel[]>
                loaderComponent={<Loader />}
                path={getContratosQueryConfig().queryKey ?? ''}
                noDataComponent={<NoDataDisplay entityName="contrato" />}
                render={({ data = [] }) => (
                  <>
                    {data?.length &&
                      data?.map(
                        ({ contrato, prestacao, qtdParcelas, matricula }) => (
                          <DashboardCardDrawer key={contrato}>
                            <DashboardCardDrawer.Header
                              icon={TabPropostasInativaIcon}
                              title={`Matrícula #${matricula}`}
                            />
                            <DashboardCardDrawer.Body>
                              <DataDisplay label="Contrato" value={contrato} />
                              <DataDisplay
                                label="Valor Parcela"
                                value={
                                  prestacao ? formatCurrency(prestacao) : ''
                                }
                              />
                              <DataDisplay
                                label="Parcelas"
                                value={qtdParcelas}
                              />
                            </DashboardCardDrawer.Body>
                          </DashboardCardDrawer>
                        ),
                      )}
                  </>
                )}
              />
            </BemErrorBoundary>
          </Drawer.Body>
        </>
      )}
    />
  );
};
