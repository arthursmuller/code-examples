export type SelectOption = {
  value: string;
  name: string;
};

export interface BemSelectProps {
  options: SelectOption[];
  multiple?: boolean;
  defaultValue?: string | string[] | undefined;
  name?: string;
  disabled?: boolean;
  isLoading?: boolean;
  onBlur?: (e) => void;
  onChange?: (e) => void;
  searchQuery?: (query) => Promise<SelectOption[]>;
  value?: string | string[] | undefined;
  retry?: () => void;
  hasError?: boolean;
  label?: string;
}
