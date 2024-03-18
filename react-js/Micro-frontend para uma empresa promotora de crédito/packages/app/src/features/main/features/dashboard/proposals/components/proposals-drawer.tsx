import { FC } from 'react';

import { Button, Icon } from '@chakra-ui/react';
import { Link as ReactRouterDomLink } from 'react-router-dom';

import {
  ArrowLeftIcon,
  ArrowRightIcon,
  ClockIcon,
} from '@pcf/design-system-icons';
import {
  getIntencaoOperacaoQueryConfig,
  IntencaoOperacaoModel,
  Resource,
} from '@pcf/core';
import {
  DefaultFormatStrings,
  Drawer,
  DrawerProps,
  formatCurrency,
  formatDate,
  NoDataDisplay,
  Loader,
  BemErrorBoundary,
} from '@pcf/design-system';
import { mainRoutePaths } from 'features/main/routes';

import { SituationProposalText } from './situation-proposal-text';

import { DashboardCardDrawer } from '../../components/dashboard-card-drawer';
import { DataDisplay } from '../../components/data-display';

export const ProposalsDrawer: FC<DrawerProps> = (props) => (
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
          title="Minhas Propostas"
          color="secondary.mid-dark"
        />
        <Drawer.Body>
          <BemErrorBoundary>
            <Resource<IntencaoOperacaoModel[]>
              loaderComponent={<Loader />}
              path={getIntencaoOperacaoQueryConfig().queryKey ?? ''}
              noDataComponent={<NoDataDisplay entityName="proposta" />}
              render={({ data = [] }) => (
                <>
                  {data?.length &&
                    data?.map(
                      ({
                        id,
                        dataInclusao,
                        rendimento,
                        prestacao,
                        prazo,
                        passosProduto,
                      }) => (
                        <DashboardCardDrawer key={id}>
                          <DashboardCardDrawer.Header
                            title={`Proposta ${id}`}
                            icon={ClockIcon}
                          />
                          <DashboardCardDrawer.Body>
                            <DataDisplay
                              label="Data de Solicitação"
                              value={formatDate(
                                new Date(dataInclusao),
                                DefaultFormatStrings.input,
                              )}
                            />
                            <DataDisplay
                              label="Matrícula"
                              value={
                                rendimento?.matricula
                                  ? `#${rendimento?.matricula}`
                                  : ''
                              }
                            />
                            <DataDisplay
                              label="Valor Parcelas"
                              value={prestacao ? formatCurrency(prestacao) : ''}
                            />
                            <DataDisplay label="Parcelas" value={prazo} />
                            <DataDisplay
                              label="Situação"
                              component={
                                <SituationProposalText
                                  passosProduto={passosProduto}
                                />
                              }
                            />
                          </DashboardCardDrawer.Body>

                          <DashboardCardDrawer.Footer
                            justifyContent="flex-end"
                            w="100%"
                          >
                            <Button
                              variant="link"
                              size="sm"
                              as={ReactRouterDomLink}
                              to={`${mainRoutePaths.RESUMO_SOLICITACAO}/${id}`}
                              fontWeight="500"
                              textDecoration="underline"
                              rightIcon={
                                <Icon
                                  display="flex"
                                  w={2}
                                  h={3}
                                  as={ArrowRightIcon}
                                />
                              }
                            >
                              Acessar Resumo
                            </Button>
                          </DashboardCardDrawer.Footer>
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
