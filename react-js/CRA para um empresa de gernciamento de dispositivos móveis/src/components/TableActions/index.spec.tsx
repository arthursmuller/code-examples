import { render } from '../../helper/test/test-utils';
import TableAction from "./index";


describe('Component MenuItem from Table Action', () => {
  it('should match with snapshot', () => {
    const { container } = render(<TableAction
      entityIntlLabel="entity"
      linkEdit='/edit'
      linkView='/view'
      openDestroy={() => false}
      onOpenMenu={() => false}
      onCloseMenu={() => false}
      moreItems={<div>moreItems</div>}
    />);
    expect(container).toMatchSnapshot();
  });

});
