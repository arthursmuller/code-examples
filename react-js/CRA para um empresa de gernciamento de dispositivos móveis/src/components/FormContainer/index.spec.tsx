import { render, screen, fireEvent } from '@testing-library/react';
import React from 'react';

import FormContainer from './index';

describe('Component FormContainer', () => {
  it('should match to snapshot', () => {
    render(<FormContainer>Children</FormContainer>);
    expect(screen).toMatchSnapshot();

    render(
      <FormContainer>
        Children
      </FormContainer>
    );
    expect(screen).toMatchSnapshot();

    render(
      <FormContainer handlePrimary={() => false} labelPrimary="Primary">
        Children
      </FormContainer>
    );
    expect(screen).toMatchSnapshot();

    render(
      <FormContainer handleSecundary={() => false} labelSecundary="Secundary">
        Children
      </FormContainer>
    );
    expect(screen).toMatchSnapshot();

    render(
      <FormContainer handleFilter={() => false} labelFilter="Filter">
        Children
      </FormContainer>
    );
    expect(screen).toMatchSnapshot();
  });

  it('should run handlePrimary when click on buttom of labelPrimary', () => {
    const handlePrimary = jest.fn();
    const FormContainerLocal = (
      <FormContainer handlePrimary={handlePrimary} labelPrimary="Primary">
        Children
      </FormContainer>
    );
    render(FormContainerLocal);

    fireEvent.click(screen.getByText('Primary'));

    expect(handlePrimary).toHaveBeenCalledTimes(1);
  });

  it('should run handleSecundary when click on buttom of labelSecundary', () => {
    const handleSecundary = jest.fn();
    const FormContainerLocal = (
      <FormContainer handleSecundary={handleSecundary} labelSecundary="Secundary">
        Children
      </FormContainer>
    );
    render(FormContainerLocal);

    fireEvent.click(screen.getByText('Secundary'));

    expect(handleSecundary).toHaveBeenCalledTimes(1);
  });

  it('should run handleFilter when click on buttom of labelFilter', () => {
    const handleFilter = jest.fn();
    const FormContainerLocal = (
      <FormContainer handleFilter={handleFilter} labelFilter="Filter">
        Children
      </FormContainer>
    );
    render(FormContainerLocal);

    fireEvent.click(screen.getByText('Filter'));

    expect(handleFilter).toHaveBeenCalledTimes(1);
  });

  it('not should run handlePrimary when click on buttom of labelPrimary is disabled', () => {
    const handlePrimary = jest.fn();
    const FormContainerLocal = (
      <FormContainer handlePrimary={handlePrimary} labelPrimary="Primary" disabledPrimary={true}>
        Children
      </FormContainer>
    );
    render(FormContainerLocal);

    fireEvent.click(screen.getByText('Primary'));

    expect(handlePrimary).not.toHaveBeenCalled();
  });

  it('not should run handleFilter when click on buttom of labelFilter is disabled', () => {
    const handleFilter = jest.fn();
    const FormContainerLocal = (
      <FormContainer handleFilter={handleFilter} labelFilter="Filter" disabledFilter={true}>
        Children
      </FormContainer>
    );
    render(FormContainerLocal);

    fireEvent.click(screen.getByText('Filter'));

    expect(handleFilter).not.toHaveBeenCalled();
  });
});
