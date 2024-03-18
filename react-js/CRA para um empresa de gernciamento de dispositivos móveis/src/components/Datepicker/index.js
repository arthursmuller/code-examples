import React from 'react';
import ReactDatePicker from 'react-datepicker';

import Date from '../Icons/Date';
import Input from '../Input';

import 'react-datepicker/dist/react-datepicker.css';
import './date-picker.css';

const DatePicker = ({ ...props }) => {
  const ExampleCustomInput = React.forwardRef(({ value, onClick }, ref) => (
    <Input
      rightElement={<Date boxSize={6} color="gray.500" onClick={onClick} />}
      inputProps={{
        onClick: onClick,
        onChange: onClick,
        value: value,
        ref: ref,
      }}
      onClick={onClick}
    >
      {value}
    </Input>
  ));

  return (
    <ReactDatePicker
      dateFormat="dd/MM/yyyy"
      customInput={<ExampleCustomInput />}
      {...props}
    />
  );
};

export default DatePicker;
