import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
interface TimePickerProps<T>{
  options: Date;
  onChange?: (value: T | T[]) => void;
  textLabel: string | JSX.Element
}
const TimePicker: <T>(props: TimePickerProps<T>) => JSX.Element = ({
  options,
  onChange,
  textLabel,
}) => {
  return (
    <>
      <DatePicker
        selected={options}
        onChange={onChange}
        showTimeSelect
        showTimeSelectOnly
        timeIntervals={15}
        timeCaption={textLabel}
        dateFormat="HH:mm"
        timeFormat={'HH:mm'}
      />
    </>
  );
};

export default TimePicker;
