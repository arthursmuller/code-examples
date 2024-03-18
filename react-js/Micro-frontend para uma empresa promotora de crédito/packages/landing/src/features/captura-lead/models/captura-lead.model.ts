export interface FormBaseProps<T> {
  onSuccess(data: T): void;
  isActionLoading?: boolean;
  showBack?: boolean;
  simpleStepsInnerButton?: boolean;
}

export interface ConvenioFormData {
  convenio: string;
}

export interface DadosClienteFormData {
  cpf?: string;
  telefone?: string;
  email?: string;
}

export interface ProdutoFormData {
  produto: string;
  requerConvenio: boolean;
}
