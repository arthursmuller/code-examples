import { FC } from 'react';

import { Button, Flex, Grid } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';
import { useHistory } from 'react-router-dom';

import { PageLayout } from '@pcf/design-system';
import { useNavigatePathUp } from 'hooks';

import { useCreateMatricula } from './use-create-matricula';
import { ListaMatriculas } from './components/lista-matriculas';
import { AlertBannerInstructions } from './components/alert-banner-instructions';

import { PerfilRoutesPaths } from '../perfil.routes.enum';

const title = 'Matrículas disponíveis';

export const Matriculas: FC = () => {
  const {
    handleSubmit,
    formState: { isValid },
    control,
  } = useForm<{
    matricula: string;
  }>({ mode: 'onChange' });
  const navigateUp = useNavigatePathUp();
  const history = useHistory();

  const navigateToNext = ({ matricula }): void =>
    matricula && history.push(`${PerfilRoutesPaths.rendimentos}/${matricula}`);

  const navigateToEdit = (matriculaId): void =>
    history.push(`${PerfilRoutesPaths.rendimentos}/${matriculaId}/edit`);

  const showCreateDialog = useCreateMatricula();

  // TODO: Quando tiver em um fluxo de simulação
  // essa variável será de acordo com a rota que o usuário vir.
  // Por enquanto manter como false.
  const isSimulationFlow = false;

  return (
    <PageLayout>
      <PageLayout.Header>
        <PageLayout.BackButton onClick={navigateUp} />
        <PageLayout.Title>{title}</PageLayout.Title>
      </PageLayout.Header>

      <PageLayout.Content>
        <Flex flexDir="column" mb={6}>
          <AlertBannerInstructions />

          {/* // TODO: caso nunca exista próxima tela, refatorar para ser apenas listagem sem form, do contrário, isolar para component genérico (e usar em simulation-matricula-form.tsx) */}
          <ListaMatriculas control={control} onEdit={navigateToEdit} />

          <Grid
            width="100%"
            gridTemplateColumns={['1fr', '1fr', '1fr 1fr 1fr']}
            gridTemplateAreas={[
              "'submit' 'create'",
              "'submit' 'create'",
              "'. create submit'",
            ]}
            gap="24px"
          >
            <Button
              gridArea="create"
              variant="link"
              color="secondary.regular"
              onClick={showCreateDialog}
            >
              Cadastrar novo Benefício
            </Button>

            {isSimulationFlow && (
              <Button
                gridArea="submit"
                disabled={!isValid}
                onClick={handleSubmit(navigateToNext)}
              >
                Continuar
              </Button>
            )}
          </Grid>
        </Flex>
      </PageLayout.Content>
    </PageLayout>
  );
};
