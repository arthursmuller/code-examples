import { UnorderedList } from '@chakra-ui/react';
import React from 'react';

import { TimelineItem, TimelineItemTitle } from '..';

import { ReportSiteDateType } from '../../../types/reports';
import TimelineSite from '../TimelineSite';

interface TimelimeProps {
  rows: ReportSiteDateType[];
}

function Timeline({ rows }: TimelimeProps) {
  return (
    <UnorderedList ml="0" display="flex" flexGrow={1} flexDirection="column">
      {rows.map((timelineItem, index) => {
        return (
          <React.Fragment key={index}>
            <TimelineItemTitle>{timelineItem.date}</TimelineItemTitle>
            {timelineItem.items?.map((item, index) => {
              return (
                <TimelineItem
                  key={`subitem-${index}`}
                  leftContent={new Date(item.accessedAt).toISOString().split('T')[1].split('.')[0]}
                  rightContent={<TimelineSite label={item.domain} />}
                  markColor={item.markColor}
                />
              );
            })}
          </React.Fragment>
        );
      })}
    </UnorderedList>
  );
}

export default Timeline;
